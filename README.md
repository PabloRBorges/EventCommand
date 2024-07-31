# Projeto de Exemplo de Command e Event

Este é um projeto de exemplo desenvolvido em C# .NET, organizado em várias camadas para demonstrar o uso do padrão Mediator com a biblioteca MediatR. O projeto está estruturado em vários componentes, cada um com uma responsabilidade específica.

## Estrutura do Projeto

- **WebApp.Api**: Contém os controladores e a configuração inicial do projeto.
- **WebApp.Application**: Contém a lógica de aplicação, incluindo comandos e eventos.
- **WebApp.Core**: Define interfaces e contratos que são implementados nas camadas de Application e Infrastructure.
- **WebApp.Domain**: Contém as entidades de domínio e eventos de domínio.
- **WebApp.Infrastructure**: Implementa a lógica de infraestrutura, como repositórios e acesso a dados.
- **WebApp**: O projeto principal que une todas as partes e configura os serviços.

## Uso do Mediator

O padrão Mediator é utilizado para desacoplar a comunicação entre os componentes da aplicação, facilitando a manutenção e testes. No projeto, a biblioteca MediatR é usada para implementar este padrão, permitindo que comandos e eventos sejam enviados e tratados sem que os emissores e receptores se conheçam diretamente.

### Como o Mediator é Configurado

No arquivo `Program.cs`, o MediatR é configurado no serviço de injeção de dependência com a seguinte linha:

```csharp
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));
```
Isso registra todos os handlers de comandos e eventos definidos no assembly de CreateProductCommandHandler.

### Exemplo de Comando e Handler
No exemplo, temos um comando chamado CreateProductCommand que é tratado por CreateProductCommandHandler. Esse handler é responsável por adicionar um novo produto ao repositório e publicar eventos após a criação.

```csharp
public class CreateProductCommandHandler : ICreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
    {
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price);
        await _productRepository.AddProductAsync(product);

        var productCreatedEvent = new ProductCreatedEvent
        {
            Name = product.Name,
            Price = product.Price
        };

        await _mediator.Publish(productCreatedEvent, cancellationToken);

        var productUpdateEvent = new ProductUpdateEvent
        {
            Name = product.Name,
            Price = product.Price
        };

        await _mediator.Publish(productUpdateEvent, cancellationToken);

        return true;
    }
}
```

### Exemplo de Evento e Handler
O evento ProductCreatedEvent é publicado pelo handler do comando. Esse evento é então tratado pelo ProductEventHandler.

```csharp
public class ProductEventHandler : IProductEventHandler
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Lógica para tratar o evento, ex.: enviar email de confirmação

        return Task.CompletedTask;
    }

    public Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
    {
        // Lógica para tratar o evento, ex.: enviar email de confirmação

        return Task.CompletedTask;
    }
}
```
## Controlador da API
O ProductController é o ponto de entrada para a API REST, permitindo a criação de produtos através de uma solicitação HTTP POST. O controller usa o MediatR para enviar o comando CreateProductCommand e tratar a resposta.

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}
```
Neste exemplo, o endpoint POST /api/product aceita um CreateProductCommand como entrada. O comando é processado pelo MediatR, que encaminha o comando para o handler apropriado. Se o comando for bem-sucedido, o resultado é Ok(), caso contrário, BadRequest().

## Executando o Projeto

Clone o Repositório:

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
Configurar a String de Conexão
Atualize a string de conexão do banco de dados no arquivo appsettings.json.
```

## Executar as Migrações:
```bash
dotnet ef database update
```
## Executar a Aplicação:
```bash
dotnet run --project WebApp
```
## Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request.
