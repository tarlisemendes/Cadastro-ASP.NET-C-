
Para a implementação do teste, foi desenvolvido dois projetos na mesma solução.

O CRUD foi feito no projeto API.Produtos, uma API dotnet core 2.2 tendo os dados persistidos em um banco Microsoft Server SQL, com EFCore e Migrations (Code First). A documentação para uso da API foi feita com o Swagger UI, a API trabalha com JSON.

O projeto Client.Produtos consome a API foi desenvolvido em dotnet core MVC com utilização do Identity para autenticação, as views são feitas com Razor e Bootstrap.

Melhorias futuras: Implementação de um arquivo Docker Compose para a criação de um Container com o banco de dados, a API e o Client para posteriormente fazer o build na AWS e hospedar todo o sistema. 


