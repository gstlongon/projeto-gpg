class Login {
    constructor() {
        this.init()
    }


    register() {
        const form = document.getElementById('registerForm')

        form.addEventListener('submit', async function (event) {
            event.preventDefault(); 
            const formData = new FormData(this);
        
            const data = {
                nome: formData.get('nome'),
                sobrenome: formData.get('sobrenome'),
                email: formData.get('email'),
                senha: formData.get('senha'),
                telefone: formData.get('telefone'),
                dataNascimento: formData.get('dataNascimento'),
                endereco: formData.get('endereco'),
                cidade: formData.get('cidade'),
                estado: formData.get('estado'),
                cep: formData.get('cep'),
                role: formData.get('role') || 'User'
            };
        
            try {
                const response = await axios.post('[URL-API]', data);
        
                Swal.fire({
                    icon: 'success',
                    title: 'Cadastro realizado com sucesso!',
                    text: response.data.message || 'Bem-vindo!',
                    confirmButtonText: 'OK'
                });
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
     
     
     





    init() {
        console.log('ola mundo')
        this.register()
    }
}

new Login()