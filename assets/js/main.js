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



    init() {
        console.log('hello world')
        this.rotateImg()
    }
}

new Main()