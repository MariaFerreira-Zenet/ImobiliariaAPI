# ImobiliariaAPI

*Princípios e Boas Práticas Utilizadas*

-Clean Code: O código é escrito de forma clara e compreensível, seguindo convenções de nomenclatura consistentes e padrões de codificação que facilitam a leitura e manutenção. Comentários e documentação são utilizados para esclarecer funcionalidades complexas, intenções e lógicas de negócio importantes.
-SOLID Principles:
Single Responsibility Principle (SRP): Cada classe e método tem uma única responsabilidade. Por exemplo, os controladores em nosso projeto apenas coordenam ações entre o modelo e a visualização, sem conter lógica de negócio diretamente.
Open/Closed Principle (OCP): O sistema é projetado para ser aberto para extensão, mas fechado para modificação. Isso é alcançado através do uso de interfaces e classes abstratas, permitindo que novos comportamentos sejam adicionados sem alterar o código existente significativamente.
Liskov Substitution Principle (LSP), Interface Segregation Principle (ISP), e Dependency Inversion Principle (DIP) são aplicados onde apropriado, especialmente na estrutura de serviços e repositórios.
-DRY (Don't Repeat Yourself): Evita a duplicação de código através do uso de métodos genéricos, classes base, e serviços reutilizáveis.


*Estrutura do Projeto*

A estrutura do projeto está organizada em várias camadas, típica em aplicações ASP.NET Core com Entity Framework:

-Presentation Layer (API Controllers): Contém os controladores que respondem a requisições HTTP, chamando os serviços apropriados e respondendo com os dados adequados.
-Data Access Layer (DAL): Implementado com Entity Framework Core, essa camada inclui o DbContext e classes de entidade que modelam a base de dados.
-Data Transfer Objects (DTOs): Usados para transferir dados entre camadas e para fora da API, garantindo que dados sensíveis não sejam expostos e que a entrada do usuário seja validada antes de processar.
-Models: As classes de entidades que são mapeadas para a base de dados.
-Migrations: Para gerenciar mudanças incrementais e versionamento da base de dados.

*Utilização do Swagger*

O projeto utiliza Swagger (OpenAPI) para documentação da API. Isso facilita a visualização e teste das interfaces API de uma forma interativa e automática.
