## Fluxo de Caixa - [Pietro Romano]

## Descrição:
Uma aplicação responsável pelo gerenciamento de fluxo de caixa, utilizando boas práticas de arquitetura de software, além da implementação do **Design Pattern ["Mediator"**] de uma forma **totalmente customizada!**


Foi escolhida a **arquitetura de microserviços** pela finalidade do projeto e simulação de ambientes que necessitam da utilização de aplicações distribuídas.

Todas as aplicações foram desenvolvidas em **.NET Core 6.0** e rodam dentro de **containers Docker** junto com todos os serviços depentes.


## Aplicações:
* API de Identidade - Autenticação e gerenciamento de usuários;
* API de Contas - Gerenciamento de contas e de gerentes de conta;
* API de Transações - Efetuar movimentações financeiras em uma conta;
* API de Relatórios - Disponibilização de relatórios;
* API de Encaminhamento de Tráfego - Responsável pelo recebimento de todas as requisições e pelo encaminhamento aos serviços responsáveis;


## Serviços:
* [RabbitMQ](https://www.rabbitmq.com/) - Comunicação assíncrona entre as aplicações;
* [MongoDB](https://www.mongodb.com/) - Extrato (Relatório de transações sincronizados com a base relacional através do padrão CQRS);
* [PostgreSQL](https://www.postgresql.org/) - Usuários / Transações / Contas / Gerentes de Conta;


## Bibliotecas:
* [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html) - Encaminhamento de tráfego externo para os serviços apropriados internos;
* [EntityFramework](https://learn.microsoft.com/en-us/ef/) - ORM para comunicação com o PostgreSQL;
* [MassTransit](https://masstransit-project.com/) - Camada de abstração para gerenciamento de mensagerias;
