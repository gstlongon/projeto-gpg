class Dashboard {
    constructor() {
        this.token = sessionStorage.getItem('token')
        if (!this.token) {
            window.location.href = 'login.html'; 
        }

        this.init()
    }

    setWelcome() {
        const decodeToken = this.decodeJWT(this.token)

        const userName = document.querySelectorAll('.dashboard__username')

        userName.forEach(item => {
            item.innerHTML = decodeToken.unique_name
        })
    }

    decodeJWT(token) {
        try {
            const parts = token.split('.');
    
            if (parts.length !== 3) {
                throw new Error('Token inválido');
            }
    
            const payloadBase64 = parts[1]
                .replace(/-/g, '+') 
                .replace(/_/g, '/'); 
    
            const decodedPayload = atob(payloadBase64);
    
            return JSON.parse(decodedPayload);
        } catch (error) {
            console.error('Erro ao decodificar JWT:', error.message);
            return null;
        }
    }


    addProduct() {
        const form = document.getElementById('productForm');

        if(!form) return
    
        form.addEventListener('submit', async (event) => {
            event.preventDefault();
    
            const formData = new FormData(form);
    
            const productData = {
                name: formData.get('nome'),
                description: formData.get('descricao'),
                price: parseFloat(formData.get('preco')) || 0
            };
    
            const token = this.token; 
            console.log(token);
    
            try {
                Swal.fire({
                    title: 'Aguarde...',
                    text: 'Estamos criando o produto.',
                    allowOutsideClick: false,
                    showConfirmButton: false,
                    didOpen: () => {
                        Swal.showLoading();
                    }
                });
    
                const productResponse = await axios.post('https://localhost:7119/api/Product', productData, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                });
    
                console.log(productResponse.data.id)
                const productResult = productResponse.data;
                const productId = productResponse.data.id; 

                console.log('ID: ', productId)

                console.log('Produto criado com sucesso:', productResult);
    
                const file = formData.get('attachments');
    
                if (file) {
                    const imageFormData = new FormData();
                    imageFormData.append('productId', productId);
                    imageFormData.append('file', file);
    
                    const imageResponse = await axios.post(`https://localhost:7119/api/Product/${productId}/UploadImage`, imageFormData, {
                        headers: {
                            'Authorization': `Bearer ${token}`,
                            'Content-Type': 'multipart/form-data'
                        }
                    });
    
                    console.log('Imagem enviada com sucesso:', imageResponse.data);
                } else {
                    console.log('Nenhuma imagem foi anexada.');
                }
    
                Swal.fire({ 
                    icon: 'success',
                    title: 'Sucesso!',
                    text: 'O produto foi criado com sucesso.',
                    showConfirmButton: true
                });
    
            } catch (error) {
                console.error('Erro:', error);
    
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!',
                    text: error.response?.data?.message || error.message || 'Ocorreu um erro ao criar o produto.',
                    showConfirmButton: true
                });
            }
        });
    }

    async loadProducts() {
        const token = this.token;
    
        try {
            Swal.fire({
                title: 'Aguarde...',
                text: 'Carregando produtos...',
                allowOutsideClick: false,
                showConfirmButton: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });
    
            const response = await axios.get('https://localhost:7119/api/Product', {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
    
            const products = response.data; 
            console.log('Produtos carregados:', products);
    
            const container = document.getElementById('dashboard-products');
            container.innerHTML = '';
    
            products.forEach(product => {
                const productHTML = `
                    <div class="col-md-3">
                        <div class="card product__card">
                            <div class="product__img-box">
                                <img src="${product.imageUrl ? 'backend/Presentation/wwwroot/' + product.imageUrl : 'assets/svg/no-img.svg'}" alt="Foto de ${product.name}">
                            </div>
                            <h3 class="product__title">${product.name}</h3>
                            <p class="product__description">${product.description}</p>
                            <p class="product__price">R$ ${product.price}</p>
                            <div class="product__buttons">
                                <button class="product-update-btn" data-id="${product.id}" data-name="${product.name}" data-description="${product.description}" data-price="${product.price}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="13" viewBox="0 0 12 13"><path d="M0,10.659v1.922A.408.408,0,0,0,.4,13H2.219a.386.386,0,0,0,.28-.123l7.493-7.92L7.612,2.441l-7.5,7.921A.432.432,0,0,0,0,10.659Z" fill="purple"/><path d="M8.341,1.431,10.651,3.88l1.124-1.192a.851.851,0,0,0,0-1.154L10.554.239a.739.739,0,0,0-1.088,0Z" fill="purple"/></svg>
                                </button>
                                <button class="product-delete-btn" data-id="${product.id}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="12" height="13" viewBox="0 0 12 13"><g transform="translate(-0.34)"><path d="M4.032,4.767a.477.477,0,0,1,.326.127.42.42,0,0,1,.135.306v5.2a.42.42,0,0,1-.135.306.483.483,0,0,1-.653,0,.42.42,0,0,1-.135-.306V5.2a.42.42,0,0,1,.135-.306A.477.477,0,0,1,4.032,4.767Zm2.308,0a.477.477,0,0,1,.326.127A.42.42,0,0,1,6.8,5.2v5.2a.42.42,0,0,1-.135.306.483.483,0,0,1-.653,0,.42.42,0,0,1-.135-.306V5.2a.42.42,0,0,1,.135-.306A.477.477,0,0,1,6.34,4.767ZM9.109,5.2a.42.42,0,0,0-.135-.306.483.483,0,0,0-.653,0,.42.42,0,0,0-.135.306v5.2a.42.42,0,0,0,.135.306.483.483,0,0,0,.653,0,.42.42,0,0,0,.135-.306Z" fill="purple"/><path d="M12.34,2.6a.841.841,0,0,1-.27.613.954.954,0,0,1-.653.254h-.462v7.8a1.681,1.681,0,0,1-.541,1.226A1.909,1.909,0,0,1,9.109,13H3.571a1.909,1.909,0,0,1-1.305-.508,1.681,1.681,0,0,1-.541-1.226v-7.8H1.263A.954.954,0,0,1,.61,3.213.841.841,0,0,1,.34,2.6V1.733a.841.841,0,0,1,.27-.613A.954.954,0,0,1,1.263.867H4.494a.841.841,0,0,1,.27-.613A.954.954,0,0,1,5.417,0H7.263a.954.954,0,0,1,.653.254.841.841,0,0,1,.27.613h3.231a.954.954,0,0,1,.653.254.841.841,0,0,1,.27.613Zm-9.583.867-.109.051v7.749a.841.841,0,0,0,.27.613.955.955,0,0,0,.653.254H9.109a.955.955,0,0,0,.653-.254.841.841,0,0,0,.27-.613V3.518l-.109-.051ZM1.263,2.6H11.417V1.733H1.263Z" fill="purple"/></g></svg>
                                </button>
                            </div>
                        </div>
                    </div>
                `;
                        
                container.innerHTML += productHTML; 
            });
    
            setTimeout(() => {
                Swal.close();
            }, 700)
    
            this.addEventListeners()
        } catch (error) {
            console.error('Erro ao carregar produtos:', error);
    
            Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: error.response?.data?.message || error.message || 'Ocorreu um erro ao carregar os produtos.',
                showConfirmButton: true
            });
        }
    }
    
    addEventListeners() {
        const updateButtons = document.querySelectorAll('.product-update-btn');
        updateButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                console.log('event', e.target)
                const target = e.target.closest('button');

                const productId = target.dataset.id;
                const productName = target.dataset.name;
                const productDescription = target.dataset.description;
                const productPrice = target.dataset.price;

    
                document.getElementById('updateName').value = productName;
                document.getElementById('updateDescription').value = productDescription;
                document.getElementById('updatePrice').value = productPrice;
    
                const updateModal = new bootstrap.Modal(document.getElementById('updateProduct'));
                updateModal.show();
    
                const updateForm = document.getElementById('updateProductForm');
                updateForm.onsubmit = async (event) => {
                    event.preventDefault();
    
                    const updatedProduct = {
                        name: document.getElementById('updateName').value,
                        description: document.getElementById('updateDescription').value,
                        price: parseFloat(document.getElementById('updatePrice').value) || 0
                    };

                    console.log('data: ', updatedProduct)
    
                    try {
                        const token = this.token;
                        await axios.put(`https://localhost:7119/api/Product/update/${productId}`, updatedProduct, {
                            headers: {
                                'Authorization': `Bearer ${token}`,
                                'Content-Type': 'application/json'
                            }
                        });
    
                        Swal.fire({
                            icon: 'success',
                            title: 'Produto atualizado!',
                            text: 'O produto foi atualizado com sucesso.',
                            showConfirmButton: true
                        });
    
                        updateModal.hide();
                        this.loadProducts();  
    
                    } catch (error) {
                        console.error('Erro ao atualizar produto:', error);
                        Swal.fire({
                            icon: 'error',
                            title: 'Erro!',
                            text: 'Ocorreu um erro ao atualizar o produto.',
                            showConfirmButton: true
                        });
                    }
                };
            });
        });
    
        const deleteButtons = document.querySelectorAll('.product-delete-btn');
        deleteButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                const target = e.target.closest('button');

                const productId = target.dataset.id;
    
                const deleteModal = new bootstrap.Modal(document.getElementById('deleteProduct'));
                deleteModal.show();
    
                document.getElementById('confirmDeleteBtn').onclick = async () => {
                    try {
                        const token = this.token;
                        await axios.delete(`https://localhost:7119/api/Product/delete/${productId}`, {
                            headers: {
                                'Authorization': `Bearer ${token}`,
                                'Content-Type': 'application/json'
                            }
                        });
    
                        Swal.fire({
                            icon: 'success',
                            title: 'Produto excluído!',
                            text: 'O produto foi excluído com sucesso.',
                            showConfirmButton: true
                        });
    
                        deleteModal.hide();
                        this.loadProducts();  
    
                    } catch (error) {
                        console.error('Erro ao excluir produto:', error);
                        Swal.fire({
                            icon: 'error',
                            title: 'Erro!',
                            text: 'Ocorreu um erro ao excluir o produto.',
                            showConfirmButton: true
                        });
                    }
                };
            });
        });
    }

    getProduct() {
        const form = document.querySelector('.dashboard__search-form');
        
        form.addEventListener('submit', (e) => { 
            e.preventDefault();
            
            const productId = document.getElementById('product').value;
            
            if (!productId) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Atenção!',
                    text: 'Por favor, insira um ID de produto!',
                });
                return;
            }
        
            axios.get(`https://localhost:7119/api/Product/${productId}`)
                .then(response => {
                    console.log(response.data)
                    const product = response.data; 
                    
                    if (product) {
                        const productHTML = `
                                <div class="col-md-3">
                                    <div class="card product__card">
                                        <div class="product__img-box">
                                            <img src="${product.imageUrl ? 'backend/Presentation/wwwroot/' + product.imageUrl : 'assets/svg/no-img.svg'}" alt="Foto de ${product.name}">
                                        </div>
                                        <h3 class="product__title">${product.name}</h3>
                                        <p class="product__description">${product.description}</p>
                                        <p class="product__price">R$ ${product.price}</p>
                                    </div>
                                </div>
                            `;
                        const container = document.getElementById('product-container');
                        container.innerHTML += productHTML;
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Produto não encontrado!',
                            text: 'Não foi possível localizar o produto com o ID fornecido.',
                        });
                    }
                })
                .catch(error => {
                    console.error('Erro ao buscar produto:', error);
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!',
                        text: 'Houve um problema ao buscar o produto.',
                    });
                });
        });
    }

    async loadData() {
        const token = this.token;
        const money = document.getElementById('money');
        const qtdProduct = document.getElementById('qtdProducts');
        let totalValue = 0; 
    
        try {
    
            const response = await axios.get('https://localhost:7119/api/Product', {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
    
            const products = response.data;
    
            products.forEach(item => totalValue += item.price);
    
            money.innerHTML = totalValue.toFixed(2);
            qtdProduct.innerHTML = products.length;
    
    
        } catch (error) {
            console.error('Erro ao carregar produtos:', error);
    
            Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: error.response?.data?.message || error.message || 'Ocorreu um erro ao carregar os produtos.',
                showConfirmButton: true
            });
        }
    }

    async loadAndUpdateUserData() {
        const user = this.decodeJWT(this.token)
        
        const token = this.token

        try {
            const response = await axios.get(`https://localhost:7119/api/User/${user.id}`);
            const userData = response.data;
    
            document.querySelector('[name="nome"]').value = userData.name;
            document.querySelector('[name="email"]').value = userData.email;
            document.querySelector('[name="telefone"]').value = userData.phoneNumber;
            document.querySelector('[name="senha"]').value = userData.password;
            document.querySelector('[name="endereco"]').value = userData.street;
            document.querySelector('[name="cidade"]').value = userData.city;
            document.querySelector('[name="estado"]').value = userData.state;
            document.querySelector('[name="cep"]').value = userData.postalCode;
    
            const form = document.querySelector('#userUpdateForm'); 
            form.addEventListener('submit', async function (e) {
                e.preventDefault();
    
                const updatedData = {
                    name: document.querySelector('[name="nome"]').value,
                    email: document.querySelector('[name="email"]').value,
                    phoneNumber: document.querySelector('[name="telefone"]').value,
                    password: document.querySelector('[name="senha"]').value, 
                    street: document.querySelector('[name="endereco"]').value,
                    number: '000',
                    city: document.querySelector('[name="cidade"]').value,
                    state: document.querySelector('[name="estado"]').value,
                    postalCode: document.querySelector('[name="cep"]').value,
                    country: 'Brazil',
                };
    
                try {
                    Swal.fire({
                        title: 'Aguarde...',
                        text: 'Atualizando dados do usuário...',
                        allowOutsideClick: false,
                        showConfirmButton: false,
                        didOpen: () => {
                            Swal.showLoading();
                        }
                    });
    
                    await axios.put(`https://localhost:7119/api/User/${user.id}`, updatedData, {
                        headers: {
                            'Authorization': `Bearer ${token}`, 
                            'Content-Type': 'application/json'
                        }
                    });
    
                    Swal.close();
    
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Os dados do usuário foram atualizados com sucesso.',
                        showConfirmButton: true
                    });
                    this.setWelcome()
    
                } catch (error) {
                    Swal.close();
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!',
                        text: error.response?.data?.message || error.message || 'Ocorreu um erro ao atualizar os dados.',
                        showConfirmButton: true
                    });
                }
            });
        } catch (error) {
            console.error('Erro ao carregar dados do usuário:', error);
            Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: 'Não foi possível carregar os dados do usuário.',
                showConfirmButton: true
            });
        }
    }

    logout() {
        const buttons = document.querySelectorAll('.dashboard__nav-logout')

        buttons.forEach(btn => {
            btn.addEventListener('click', () => {
                sessionStorage.removeItem('token')
                window.location.href = 'login.html'; 
            })
        })
    }

    init() {
        console.log(this.token)
        this.setWelcome()
        if (window.location.href.includes('dashboard-produtos.html')) {
            this.addProduct();
            this.loadProducts();
        }

        if (window.location.href.includes('dashboard.html')) {
            this.getProduct()
            this.loadData()
        }

        if (window.location.href.includes('dashboard-meus-dados.html')) {
            this.loadAndUpdateUserData()
        }

        this.logout()


        
    }
}

new Dashboard
