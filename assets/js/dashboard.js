class Dashboard {
    constructor() {
        this.token = sessionStorage.getItem('token')
        this.init()
    }

    init() {
        console.log('ola mundo')
        if (!this.token) {
            window.location.href = 'login.html'; 
        }
    }
}

new Dashboard
