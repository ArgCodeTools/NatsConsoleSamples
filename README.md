
# NatsConsoleSamples

Este proyecto incluye varios ejemplos de consumers y publishers de NATS con C# (.NET 9).


## Estructura de proyectos

- **ConsumerWithJetStream**: Ejemplos de consumers JetStream.
- **PublisherWithJetStream**: Ejemplo de publisher de mensajes en JetStream.
- **SubscriberWithMessaging**: Ejemplo de suscripción básica a subjects NATS.


## Ejemplos de consumers JetStream

### 1. ConsumerWithDeliveryGroup
- Utiliza dos consumers en paralelo, ambos pertenecen al mismo grupo de entrega (`workers`).
- Permite balanceo de carga: los mensajes se distribuyen entre ambos consumers.
- Útil para procesamiento concurrente y balanceado de mensajes.

### 2. BasicConsumer
- Utiliza un solo consumer sin grupo de entrega.
- Todos los mensajes del stream son procesados por este único consumer.
- Es el ejemplo más simple de consumo JetStream, ideal para pruebas o procesamiento secuencial.


## Ejemplo de Publisher

El proyecto `PublisherWithJetStream` permite publicar mensajes en el stream configurado (`EVENTS-SAMPLE`). Útil para probar el flujo de mensajes y el consumo por parte de los consumers.


## Ejemplo de Subscriber

El proyecto `SubscriberWithMessaging` muestra cómo suscribirse a un subject NATS y procesar mensajes de forma sencilla, sin JetStream.

## Requisitos previos

- .NET 9 SDK
- Docker (opcional, para levantar el servidor NATS)

## Levantar el servidor NATS con Docker

Puedes iniciar un servidor NATS local usando el archivo de docker-compose incluido:

```powershell
cd docker-compose
docker-compose up -d
```

Esto levantará el servidor NATS en `nats://127.0.0.1:4222` con configuración JetStream.

## Cómo ejecutar los ejemplos

Desde la línea de comandos, navega al proyecto que deseas ejecutar y usa `dotnet run` con el argumento correspondiente:


### Ejecutar consumers JetStream


```powershell
cd src/ConsumerWithJetStream
dotnet run
```

Al ejecutar el comando, se mostrará un menú interactivo donde podrás seleccionar el tipo de consumidor:

- Escribe `1` para ejecutar el ejemplo con grupo de entrega (`deliverygroup`).
- Escribe `2` para ejecutar el consumer básico (`basic`).


### Ejecutar Publisher

```powershell
cd src/PublisherWithJetStream
dotnet run
```


### Ejecutar Subscriber

```powershell
cd src/SubscriberWithMessaging
dotnet run
```

## Notas

- Asegúrate de que el servidor NATS esté corriendo antes de ejecutar los ejemplos.
- Puedes modificar los nombres de streams, subjects y grupos en el código fuente según tus necesidades.

---
Para dudas o mejoras, abre un issue o PR.
