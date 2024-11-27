class Dashboard {
    constructor() {
        this.token = sessionStorage.getItem('token')
        if (!this.token) {
            window.location.href = 'login.html'; 
        }
        this.init()
    }

    init() {
        console.log('ola mundo')
        
    }
}

new Dashboard
