PREMESSA: tutto ciò è stato creato con lo scopo di dare un'idea basilare di ASP NET CORE nel mio poco tempo libero a disposizione tra un week end e l'altro.

Questa soluzione ospita all'interno un'implementazione di ApiGateway. Esplorando i vari file di configurazione Ocelot.json (che variano in base all'ambiente) si possono notare le rotte:

{
"Routes": [
  {
    "DownstreamPathTemplate": "/api/v1/Authentication/Token",
    "DownstreamScheme": "https",
    "DownstreamHostAndPorts": [
      {
        "Host": "authentication",
        "Port": 444
      }
    ],
    "UpstreamPathTemplate": "/api/v1/ApiPortal/Authentication/Token",
    "UpstreamHttpMethod": [ "POST" ]
  }
]
}

Il DownstreamPathTemplate rappresenta l'endpoint dell'api che effettivamente dietro le quinte verrà chiamata. L'UpstreamPathTemplate invece rappresenta proprio l'endpoint del nostro api gateway: questo
significa che esponendo, ad esempio, il nostro api gateway sulla porta 443 utilizzando il protocollo https qualsiasi richiesta inviata a "/api/v1/ApiPortal/Authentication/Token" verrà reindirizzata
a "/api/v1/Authentication/Token". Con questo Api Gateway, e quindi con gli Api Gateway in generale, possiamo avere svariati vantaggi come ad esempio un unico punto d'accesso, la gestione del traffico,
miglioramento delle prestazioni ecc ecc.

Argomenti chiave trattati:
#Ocelot
#Docker
#.Net 8
#Api Gateway
