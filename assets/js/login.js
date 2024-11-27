class Login {
    constructor() {
        this.token = sessionStorage.getItem('token')
        if (this.token) {
            window.location.href = 'dashboard.html'; 
        }

        this.init()
    }


    register() {
        const form = document.getElementById('registerForm');
    
        if (!form) {
            console.error('Formulário de registro não encontrado!');
            return;
        }
    
        form.addEventListener('submit', async function (event) {
            event.preventDefault(); 
            const formData = new FormData(this); 
    
            const data = {
                name: formData.get('nome'), 
                email: formData.get('email'),
                phoneNumber: formData.get('telefone'),
                password: formData.get('senha'), 
                street: formData.get('endereco'), 
                number: formData.get('numero') || '', 
                city: formData.get('cidade'),
                state: formData.get('estado'),
                postalCode: formData.get('cep'), 
                country: formData.get('pais') || 'Brasil', 
                dateOfBirth: formData.get('dataNascimento') || '', 
                role: formData.get('role') || 'User'
            };
    
            try {
                const response = await axios.post(
                    'https://localhost:7119/api/User',
                    data,
                    {
                        headers: {
                            'Content-Type': 'application/json', 
                        }
                    }
                );
    
                Swal.fire({
                    icon: 'success',
                    title: 'Cadastro realizado com sucesso!',
                    text: response.data.message || 'Bem-vindo!',
                    confirmButtonText: 'OK'
                }).then(() => {
                    const bootstrapModal = document.getElementById('loginModal');
                    const modalInstance = bootstrap.Modal.getInstance(bootstrapModal);
                    if (modalInstance) {
                        modalInstance.hide();
                    }})
    
                form.reset();

            } catch (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro no cadastro',
                    text: error.response?.data?.message || 'Não foi possível realizar o cadastro. Tente novamente.',
                    confirmButtonText: 'OK'
                });
            }
        });
    }

    async login() {
        const form = document.getElementById('loginForm');
    
        if (!form.dataset.listenerAdded) {
            form.dataset.listenerAdded = true;
    
            form.addEventListener('submit', async function (event) {
                event.preventDefault();
    
                Swal.fire({
                    title: 'Aguarde...',
                    text: 'Verificando credenciais',
                    allowOutsideClick: false,
                    didOpen: function () {
                        Swal.showLoading();
                    }
                });
    
                const formData = new FormData(this);
                const data = {
                    email: formData.get('user'),
                    password: formData.get('password')
                };
    
                try {
                    const response = await axios.post(
                        'https://localhost:7119/api/Auth/signIn', 
                        data,
                        {
                            headers: {
                                'Content-Type': 'application/json', 
                            }
                        }
                    );
    
                    const token = response.data;
                    if (token) {
                        sessionStorage.setItem('token', token);
    
                        Swal.fire({
                            icon: 'success',
                            title: 'Login realizado com sucesso!',
                            text: 'Redirecionando para o dashboard...',
                            timer: 2000, 
                            showConfirmButton: false 
                        });
    
                        setTimeout(() => {
                            window.location.href = '/dashboard.html'; 
                        }, 2000);
                    } else {
                        throw new Error('Token não encontrado na resposta.');
                    }
                } catch (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro no login',
                        text: error.response?.data?.message || 'Credenciais inválidas. Tente novamente.',
                        confirmButtonText: 'OK'
                    });
                }
            });
        }
    }
    
    
    
     
     
     





    init() {
        console.log('ola mundo')
        this.register()
        this.login()
    }
}

new Login()