class Main {
    constructor() {
        this.init()
    }


    rotateImg() {
        const productViewer = document.querySelectorAll('.index__new-item');

        productViewer.forEach(item => {
            const rotatingImage = item.querySelector('.index__new-img');
            const totalFrames = 2; 
            let currentFrame = 1;
    
            item.addEventListener('mousemove', (event) => {
                const rect = item.getBoundingClientRect();
                const xPos = event.clientX - rect.left;
                const percentage = xPos / rect.width;
                const frame = Math.min(totalFrames, Math.max(1, Math.ceil(percentage * totalFrames)));
                
                if (frame !== currentFrame) {
                    rotatingImage.src = `assets/img/index-new-img${frame}.png`;
                    currentFrame = frame;
                }
            });

        })
    }

    startCountdown(initialTime) {
        TweenLite.defaultEase = Expo.easeOut;
    
        const timerEl = document.querySelector('.timer--clock'); 
    
        initTimer(initialTime);
    
        function initTimer(timeString) {
            const hoursGroupEl = timerEl.querySelector('.hours-group');
            const minutesGroupEl = timerEl.querySelector('.minutes-group');
            const secondsGroupEl = timerEl.querySelector('.seconds-group');
    
            const hoursGroup = {
                firstNum: hoursGroupEl.querySelector('.first'),
                secondNum: hoursGroupEl.querySelector('.second')
            };
    
            const minutesGroup = {
                firstNum: minutesGroupEl.querySelector('.first'),
                secondNum: minutesGroupEl.querySelector('.second')
            };
    
            const secondsGroup = {
                firstNum: secondsGroupEl.querySelector('.first'),
                secondNum: secondsGroupEl.querySelector('.second')
            };
    
            let time = {
                hour: timeString.split(':')[0],
                min: timeString.split(':')[1],
                sec: timeString.split(':')[2]
            };
    
            let timeNumbers;
    
            function updateTimer() {
                let timestr;
                const date = new Date();
    
                date.setHours(time.hour);
                date.setMinutes(time.min);
                date.setSeconds(time.sec);
    
                const newDate = new Date(date.valueOf() - 1000);
                const temp = newDate.toTimeString().split(" ");
                const tempsplit = temp[0].split(':');
    
                time.hour = tempsplit[0];
                time.min = tempsplit[1];
                time.sec = tempsplit[2];
    
                timestr = time.hour + time.min + time.sec;
                timeNumbers = timestr.split('');
    
                updateTimerDisplay(timeNumbers);
    
                if (timestr === '000000') {
                    countdownFinished();
                } else {
                    setTimeout(updateTimer, 1000);
                }
            }
    
            function updateTimerDisplay(arr) {
                animateNum(hoursGroup.firstNum, arr[0]);
                animateNum(hoursGroup.secondNum, arr[1]);
                animateNum(minutesGroup.firstNum, arr[2]);
                animateNum(minutesGroup.secondNum, arr[3]);
                animateNum(secondsGroup.firstNum, arr[4]);
                animateNum(secondsGroup.secondNum, arr[5]);
            }
    
            // Anima o grupo de números
            function animateNum(group, arrayValue) {
                TweenMax.killTweensOf(group.querySelector('.number-grp-wrp'));
                TweenMax.to(group.querySelector('.number-grp-wrp'), 1, {
                    y: -group.querySelector('.num-' + arrayValue).offsetTop
                });
            }
    
            setTimeout(updateTimer, 1000);
        }
    }

    changeProduct() {
        const btnRed = document.querySelector('.red')
        const btnPink = document.querySelector('.pink')
        const bgRed = document.querySelector('.index__change-bg-red')
        const bgPink = document.querySelector('.index__change-bg-pink')
        const productRed = document.querySelector('.index__change-img-red')
        const productPink = document.querySelector('.index__change-img-pink')

        btnRed.addEventListener('click', () => {
            btnRed.classList.add('active')
            bgRed.classList.add('active')
            setTimeout(() => {
                productRed.classList.add('active')

            }, 300)

            btnPink.classList.remove('active')
            bgPink.classList.remove('active')
            productPink.classList.remove('active')
        })

        btnPink.addEventListener('click', () => {
            btnPink.classList.add('active')
            bgPink.classList.add('active')
            setTimeout(() => {
                productPink.classList.add('active')

            }, 300)

            btnRed.classList.remove('active')
            bgRed.classList.remove('active')
            productRed.classList.remove('active')
        })


    }
    
     
     
     





    init() {
        console.log('hello world')
        this.rotateImg()
        this.startCountdown("12:01:11")
        this.changeProduct()

    }
}

new Main()