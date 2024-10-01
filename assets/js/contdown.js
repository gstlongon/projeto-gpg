function Countdown() {
    // DOM Element
    this.el = document.querySelector('.countdown');

    // Params
    this.countdown_interval = null;
    this.total_seconds = 0;

    // Initialize the countdown  
    this.init();
}

Countdown.prototype.init = function() {
    // DOM Elements
    this.hours = this.el.querySelectorAll('.bloc-time.hours .figure');
    this.minutes = this.el.querySelectorAll('.bloc-time.min .figure');
    this.seconds = this.el.querySelectorAll('.bloc-time.sec .figure');

    // Init countdown values
    this.values = {
        hours: parseInt(this.hours[0].parentNode.getAttribute('data-init-value')),
        minutes: parseInt(this.minutes[0].parentNode.getAttribute('data-init-value')),
        seconds: parseInt(this.seconds[0].parentNode.getAttribute('data-init-value')),
    };

    // Initialize total seconds
    this.total_seconds = this.values.hours * 60 * 60 + (this.values.minutes * 60) + this.values.seconds;

    // Animate countdown to the end 
    this.count();    
};

Countdown.prototype.count = function() {
    var that = this;

    this.countdown_interval = setInterval(function() {
        if (that.total_seconds > 0) {
            --that.values.seconds;              

            if (that.values.minutes >= 0 && that.values.seconds < 0) {
                that.values.seconds = 59;
                --that.values.minutes;
            }

            if (that.values.hours >= 0 && that.values.minutes < 0) {
                that.values.minutes = 59;
                --that.values.hours;
            }

            // Update DOM values
            // Hours
            that.checkHour(that.values.hours, that.hours[0], that.hours[1]);

            // Minutes
            that.checkHour(that.values.minutes, that.minutes[0], that.minutes[1]);

            // Seconds
            that.checkHour(that.values.seconds, that.seconds[0], that.seconds[1]);

            --that.total_seconds;
        } else {
            clearInterval(that.countdown_interval);
        }
    }, 1000);    
};

Countdown.prototype.animateFigure = function(el, value) {
    var top = el.querySelector('.top');
    var bottom = el.querySelector('.bottom');
    var back_top = el.querySelector('.top-back');
    var back_bottom = el.querySelector('.bottom-back');

    // Atualiza o valor de back
    back_top.querySelector('span').innerHTML = value;
    back_bottom.querySelector('span').innerHTML = value;

    // Animação: deslizar de baixo para cima com fade
    gsap.fromTo(top, 
        { y: 50, opacity: 0 }, // Ponto inicial (desliza de baixo para cima)
        { 
            y: 0, 
            opacity: 1, 
            duration: 0.8, 
            ease: "power2.out", 
            onComplete: function() {
                top.innerHTML = value;
                bottom.innerHTML = value;
                gsap.set(top, { y: 0, opacity: 1 }); // Reinicia a posição
            }
        }
    );

    gsap.fromTo(back_top, 
        { y: 50, opacity: 0 }, // Ponto inicial para a parte traseira
        { 
            y: 0, 
            opacity: 1, 
            duration: 0.8, 
            ease: "power2.out", 
            clearProps: 'all' 
        }
    );    
}

Countdown.prototype.checkHour = function(value, el_1, el_2) {
    var val_1 = value.toString().charAt(0);
    var val_2 = value.toString().charAt(1);
    var fig_1_value = el_1.querySelector('.top').innerHTML;
    var fig_2_value = el_2.querySelector('.top').innerHTML;

    if (value >= 10) {
        // Animate only if the figure has changed
        if (fig_1_value !== val_1) this.animateFigure(el_1, val_1);
        if (fig_2_value !== val_2) this.animateFigure(el_2, val_2);
    } else {
        // If we are under 10, replace first figure with 0
        if (fig_1_value !== '0') this.animateFigure(el_1, 0);
        if (fig_2_value !== val_1) this.animateFigure(el_2, val_1);
    }    
};

// Let's go!
const countdown = new Countdown();
