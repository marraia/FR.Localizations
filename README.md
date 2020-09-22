# Utilização do FR.Localizations em aplicações em .Net Core
![.NET Core](https://github.com/marraia/FR.Localizations/workflows/.NET%20Core/badge.svg?branch=master)

Em sua API, exiba as mensagens multi-linguagem para seus projetos!

## Injetar o uso do StringLocalization da biblioteca FR.Localizations em sua aplicação

Faça a instalação da biblioteca via nuget:
**Install-Package FR.Localizations**

No arquivo Startup.cs de sua aplicação adicione no método **ConfigureServices** o middleware em específico:
```
        public void ConfigureServices(IServiceCollection services)
        {
            ..
            ..
            services.AddFRLocalizations();
        }
```
## Criação dos arquivos multi-linguagem 

Em sua API, crie a pasta **App_Data** e adicione arquivos de nome **resources.{language}.json**
O language será nome da linguagem que você passará na header da requisição na chave Accept-Language. 
Ex: 
pt-BR (resources.pt-BR.json) - Português Brasil
en-US (resources.en-US.json) - Inglês

Nos arquivos, o conteúdo deverá estar desta forma:
**resources.pt-BR.json**
```
[
  {
    "Key": "MandatoryData",
    "Value": "Dados obrigatórios"
  },
  {
    "Key": "UserUnder",
    "Value": "Usuário {0} com menos de 18 anos!"
  }
]
```
**resources.en-US.json**
```
[
  {
    "Key": "MandatoryData",
    "Value": "Mandatory data!"
  },
  {
    "Key": "UserUnder",
    "Value": "User {0} under 18!"
  }
]
```

## Criação de Enuns com chaves das mensagens
Como estamos usando um arquivo, de chave(Key) e valor(Value), necessitamos criar essas chaves no seu projeto. 
Pode-se criar como uma boa prática um Enum, contendo essas chaves pré-definidas
Ex: Enum KeyMessage
```
public class KeyMessage
{
    public const string MandatoryData = "MandatoryData";
    public const string UserUnder = "UserUnder";
}

```

No exemplo abaixo estamos usando um exemplo, utilizando o Marraia.Notifications, para criar notificações ao invés de usarmos exceções, quando temos que validar alguma regra de negócio em nosso projeto. 
Veja a utilização em: https://github.com/marraia/marraia.notifications

Com o Enum KeyMessage, que vamos usar neste exemplo, colocamos a chave específica da mensagem que vamos usar.

```
public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly ISmartNotification _notification;
    private readonly IStringLocalization _localization;

    public UserAppService(
      ISmartNotification notification,
      IUserRepository userRepository,
      IStringLocalization localization)
    {
        _notification = notification;
        _userRepository = userRepository;
        _localization = localization;
    }
    
    public async Task InsertAsync(UserInput user)
    {
        if (string.IsNullOrEmpty(user.Login) 
                || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.Name))
        {
             _notification.NewNotificationBadRequest(_localization[KeyMessage.MandatoryData]);
             return;
        }
        
        if (user.Age < 18)
        {
            _notification.NewNotificationConflict(_localization[KeyMessage.UserUnder, user.Name]);
            return;
        }

        var login = new User(user.Login, 
                             user.Password, user.Name);

        await _userRepository
                .InsertAsync(login)
                .ConfigureAwait(false);
    }
}
```  




