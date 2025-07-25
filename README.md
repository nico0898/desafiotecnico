# 🎉 Birthday Rooms API & Frontend

Sistema de reservas para salones de cumpleaños, desarrollado con Clean Architecture, principios SOLID y almacenamiento en memoria.

## 🧱 Tecnologías utilizadas

### Backend (.NET 8)
- ASP.NET Core
- Clean Architecture
- SOLID Principles
- Almacenamiento en memoria (`InMemoryReservationRepository`)
- xUnit para tests unitarios

### Frontend (React + Vite)
- React (sin TypeScript)
- Vite
- Bootstrap 5
- FullCalendar (visualización de reservas)

---

## 🚀 Cómo ejecutar el proyecto

### 1. Clonar el repositorio
```bash
git clone https://github.com/tu_usuario/birthday-rooms.git
cd birthday-rooms
```

---

### 2. Ejecutar el Backend

#### 📁 Navegar a la carpeta de la API
```bash
cd BirthdayRooms.API
```

#### 🔧 Restaurar paquetes y correr
```bash
dotnet restore
dotnet run
```

La API estará disponible en:

```
https://localhost:5233
```

---

### 3. Ejecutar el Frontend

#### 📁 Navegar a la carpeta del frontend
```bash
cd Web
```

#### 📦 Instalar dependencias
```bash
npm install
```

#### ▶️ Ejecutar app
```bash
npm run dev
```

El frontend estará disponible en:

```
http://localhost:5173
```

---

## 🧪 Ejecutar pruebas unitarias

```bash
cd UTests
dotnet test
```

Las pruebas validan toda la lógica de negocio (validaciones de horario, superposición, salas, etc.).

---

## 📸 Funcionalidades

- Crear reservas seleccionando fecha, horario, salón y nombre del cliente.
- Visualizar las reservas del día en un calendario FullCalendar (modo día).
- Validaciones:
  - Horario entre 9:00 y 18:00
  - Sala entre 1 y 3
  - 30 minutos de diferencia entre turnos
  - No se permite superposición
- Testeado con xUnit

---

## ✅ Estructura de carpetas

```
📦 api                       → Proyecto principal ASP.NET Core
📦 Application               → Servicios de negocio
📦 Domain                    → Entidades y contratos (interfaces)
📦 Infrastructure            → Repositorio en memoria
📦 UTests                    → Tests unitarios con xUnit
📦 Web                       → Frontend en React + Vite
```
