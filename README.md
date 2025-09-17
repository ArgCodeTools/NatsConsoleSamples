# 🚀 NatsConsoleSamples

Este proyecto incluye varios ejemplos de **consumidores** y **publicadores** de NATS utilizando **C# (.NET 9)**. Está diseñado para ayudarte a entender cómo trabajar con NATS y JetStream de forma práctica y modular.

---

## 📁 Estructura de Proyectos

- `ConsumerWithJetStream`: Ejemplos de consumidores JetStream.
- `PublisherWithJetStream`: Ejemplo de publicación de mensajes en JetStream.
- `SubscriberWithMessaging`: Ejemplo de suscripción básica a subjects NATS (sin JetStream).

---

## 🎯 Ejemplos de Consumers JetStream

### 1️⃣ ConsumerWithDeliveryGroup

- Utiliza dos consumidores en paralelo dentro del mismo grupo de entrega (`workers`).
- Permite balanceo de carga: los mensajes se distribuyen entre ambos.
- Ideal para procesamiento concurrente y escalable.

### 2️⃣ BasicConsumer

- Un único consumidor sin grupo de entrega.
- Procesa todos los mensajes del stream de forma secuencial.
- Útil para pruebas simples o flujos de procesamiento lineal.

---

## ✉️ Ejemplo de Publisher

El proyecto `PublisherWithJetStream` permite publicar mensajes en el stream configurado (`EVENTS-SAMPLE`). Es ideal para probar el flujo de mensajes y verificar el comportamiento de los consumidores.

---

## 📡 Ejemplo de Subscriber

El proyecto `SubscriberWithMessaging` muestra cómo suscribirse a un subject NATS y procesar mensajes de forma directa, sin usar JetStream. Es útil para casos simples de mensajería.

---

## ✅ Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) (opcional, para levantar el servidor NATS local)

---

## 🐳 Levantar el servidor NATS con Docker

Podés iniciar un servidor NATS local usando el archivo `docker-compose` incluido:

```powershell
cd docker-compose
docker-compose up -d
```

Esto levantará el servidor NATS en `nats://127.0.0.1:4222` con configuración JetStream.

## ▶️ Cómo ejecutar los ejemplos

Desde la terminal, navegá al proyecto deseado y ejecutá con `dotnet run`:

### 🧪 Ejecutar Consumers JetStream

```bash
cd src/ConsumerWithJetStream
dotnet run
```

Al ejecutar el comando, se mostrará un menú interactivo donde podrás seleccionar el tipo de consumidor:

- Escribe `1` para ejecutar el ejemplo con grupo de entrega (`deliverygroup`).
- Escribe `2` para ejecutar el consumer básico (`basic`).


### 📤 Ejecutar Publisher

```bash
cd src/PublisherWithJetStream
dotnet run
```
Este ejemplo publica mensajes en el stream EVENTS-SAMPLE.

### 📥 Ejecutar Subscriber

```powershell
cd src/SubscriberWithMessaging
dotnet run
```
Este ejemplo se suscribe directamente a un subject NATS sin usar JetStream.

---

## 📝 Notas

- Asegúrate de que el servidor NATS esté corriendo antes de ejecutar los ejemplos.
- Puedes modificar los nombres de streams, subjects y grupos en el código fuente según tus necesidades.

## 🛠️ Configuración de Retention y Consumidores en Streams

JetStream permite configurar cómo se retienen los mensajes en un stream y cómo los consumidores interactúan con ellos. Esta sección detalla los tipos de retención disponibles y las reglas clave para configurar consumidores, especialmente en modo **WorkQueue**.

### 📦 Tipos de Retención (`Retention`)

Al definir un stream, podés elegir entre tres modos de retención:

| Tipo         | Descripción                                                                 |
|--------------|------------------------------------------------------------------------------|
| **Limits**   | Retiene mensajes según límites de cantidad, tamaño o tiempo configurados.   |
| **Interest** | Retiene mensajes mientras haya consumidores activos interesados.            |
| **WorkQueue**| Retiene mensajes hasta que sean consumidos y confirmados (ack). Ideal para distribución de tareas. |

> 💡 *WorkQueue* es especialmente útil cuando querés distribuir mensajes entre múltiples workers de forma eficiente.

---

### 👥 Configuración de Consumidores en Streams tipo WorkQueue

Cuando el stream usa `Retention = WorkQueue`, se aplican reglas específicas para los consumidores:

#### 🔹 1. Consumidor único (sin filtro de subject)

- Solo se permite **un consumidor sin filtro** (`FilterSubject` vacío o igual al subject del stream).
- Este consumidor recibe **todos los mensajes** publicados en el stream.

#### 🔹 2. Consumidores filtrados por subject

- Podés crear **varios consumidores**, cada uno con un `FilterSubject` distinto.
- Cada consumidor recibe **solo los mensajes** que coincidan con su filtro.
- No se permite más de un consumidor con el mismo filtro en un stream WorkQueue.

---

### 📌 Reglas importantes

- El `FilterSubject` debe coincidir con el subject del stream o ser un subconjunto válido.
- No se pueden crear múltiples consumidores con el mismo filtro en modo WorkQueue.
- Si necesitás paralelismo, usá **grupos de entrega** (`DeliveryGroup`) dentro de un mismo consumidor.

---

### 📚 Recursos adicionales

Para más detalles técnicos, consultá la [documentación oficial de NATS JetStream](https://docs.nats.io/jetstream/concepts/consumers#work-queue-consumers).

