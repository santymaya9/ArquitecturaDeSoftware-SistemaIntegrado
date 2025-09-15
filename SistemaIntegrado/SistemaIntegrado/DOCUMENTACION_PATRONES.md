# DOCUMENTACION DE PATRONES DE DISEÑO - SISTEMA INTEGRADO DE ALERTAS MEDICAS

## RESUMEN GENERAL

**Sistema**: Sistema Integrado de Alertas Médicas
**Tecnología**: .NET 8, C# 12.0
**Total de Patrones Implementados**: 5 patrones GoF
**Arquitectura**: Sistema médico con funcionalidades CRUD completas

---

## 1. PATRON SINGLETON

### Ubicación
- **Funcionalidad**: `Clases/Singleton/`

### Componentes

#### Clases Principales
- **SistemaIntegrado.cs**
  - Constructor privado
  - Instancia estática privada
  - Método público estático:
    - `GetInstance()` - Obtiene la única instancia del sistema
  - Propiedades públicas:
    - `Nombre { get; set; }` - Nombre del sistema
    - `Telefono { get; set; }` - Teléfono del sistema
    - `L_cuentas { get; set; }` - Lista de cuentas registradas
    - `L_centroMedico { get; set; }` - Lista de centros médicos

### Propósito
Garantizar una sola instancia del sistema integrado de alertas médicas en toda la aplicación.

---

## 2. PATRON FACTORY METHOD

### Ubicación
- **Funcionalidad**: `Funcionalidad/Crear/Factory/`

### Componentes

#### Interfaces
- **IPerfilFactory.cs**
  - `CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)` - Método de creación genérico

#### Clases Implementadoras
- **AdminFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Administrador

- **OperadorFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Operador

- **ParamedicoFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Paramédico

- **PacienteFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Paciente

### Inyecciones
- Se inyectan en servicios de creación según el tipo de perfil requerido

### Propósito
Crear diferentes tipos de perfiles médicos (Admin, Operador, Paramédico, Paciente) de forma estandarizada y desacoplada.

---

## 3. PATRON OBSERVER (Doble Implementación)

### 3.1 Observer para Creación de Alertas

#### Ubicación
- **Funcionalidad**: `Funcionalidad/Crear/Observer/`

#### Componentes

##### Interfaces
- **ICreacionObserver<T>.cs**
  - `OnElementoCreado(T elemento)` - Se ejecuta cuando se crea un nuevo elemento

##### Clases
- **AlertaCreacionObserver.cs**
  - `OnElementoCreado(Alerta elemento)` - Maneja la notificación cuando se crea una nueva alerta médica

### 3.2 Observer para Comunicaciones

#### Ubicación
- **Funcionalidad**: `Funcionalidad/Comunicacion/Observer/`

#### Componentes

##### Interfaces
- **INotificacionObserver.cs**
  - `OnNotificacionEnviada(string mensaje, Perfil destinatario)` - Se ejecuta cuando se envía una notificación

##### Clases
- **LogComunicacionObserver.cs**
  - `OnNotificacionEnviada(string mensaje, Perfil destinatario)` - Registra las comunicaciones en logs

##### Inyecciones
- **ComunicacionOperador.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicación para operadores

- **ComunicacionParamedico.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicación para paramédicos

- **ComunicacionPaciente.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicación para pacientes

### Propósito
- **Creación**: Notificar cuando se crean nuevas alertas médicas
- **Comunicación**: Registrar y notificar eventos de comunicación (llamadas, mensajes)

---

## 4. PATRON DECORATOR

### Ubicación
- **Funcionalidad**: `Funcionalidad/Mostrar/Decorator/`

### Componentes

#### Interfaces Base
- **IMostrar<T>.cs**
  - `Mostrar(T entidad)` - Método base para mostrar cualquier entidad

#### Clases Decorator

##### Clase Base Abstracta
- **MostrarDecorator<T>.cs**
  - Constructor que recibe `IMostrar<T> mostrarComponente`
  - `virtual Mostrar(T entidad)` - Implementación base que delega al componente

##### Decorators Concretos
- **FormatoJsonDecorator<T>.cs**
  - `override Mostrar(T entidad)` - Añade formato JSON con timestamp
  - Maneja entidades nulas con mensaje de error en JSON

- **FormatoXmlDecorator<T>.cs**
  - `override Mostrar(T entidad)` - Añade formato XML con timestamp
  - Escapa caracteres especiales para XML válido

- **EncabezadoDecorator<T>.cs**
  - Constructor adicional: `(IMostrar<T> mostrarComponent, string titulo, string usuario)`
  - `override Mostrar(T entidad)` - Añade encabezados profesionales con:
    - Título del reporte
    - Fecha y hora
    - Usuario responsable
    - Líneas separadoras

- **EmergenciaDecorator<T>.cs**
  - `override Mostrar(T entidad)` - Añade formato especial para emergencias:
    - Para alertas críticas (nivel ? 2): formato de emergencia con asteriscos
    - Para otras entidades: formato de emergencia estándar

#### Inyecciones
- **MostradorAdmin.cs**
  - `MostrarCentroMedico { get; }` - Para mostrar centros médicos
  - `MostrarSistemaIntegrado { get; }` - Para mostrar información del sistema
  - `MostrarCuenta { get; }` - Para mostrar cuentas de usuario
  - `MostrarAlerta { get; }` - Para mostrar alertas médicas

- **MostradorOperador.cs**
  - `MostrarAlerta { get; }` - Solo puede mostrar alertas

- **MostradorParamedico.cs**
  - `MostrarAlerta { get; }` - Solo puede mostrar alertas

### Propósito
Añadir diferentes formatos de visualización (JSON, XML, encabezados, emergencias) a la información médica sin modificar las clases originales.

---

## 5. PATRON MEMENTO

### Ubicación
- **Funcionalidad**: `Funcionalidad/Actualizar/Memento/`

### Componentes

#### Interfaces
- **IMemento.cs**
  - `ObtenerDescripcion()` - Descripción del estado guardado
  - `FechaCreacion { get; }` - Cuándo se creó el memento
  - `UsuarioResponsable { get; }` - Quién hizo el cambio

- **IActualizar<T, TCambio>.cs**
  - `Actualizar(T entidad, TCambio cambio)` - Método de actualización genérico

#### Clases Memento (Estados Guardados)

##### Para Alertas Médicas
- **AlertaMemento.cs**
  - Constructor: `(Alerta alerta, string usuarioResponsable)`
  - Estados guardados: tipo, reportante, estado, nivel triaje, fechas, equipo, rutas
  - `RestaurarEstado(Alerta alerta)` - Restaura todos los campos de la alerta
  - `ObtenerDescripcion()` - Descripción del estado de la alerta
  - Propiedades: `FechaCreacion`, `UsuarioResponsable`

##### Para Centros Médicos
- **CentroMedicoMemento.cs**
  - Constructor: `(CentroMedico centro, string usuarioResponsable)`
  - Estados guardados: nombre, teléfono, complejidad, latitud, longitud
  - `RestaurarEstado(CentroMedico centro)` - Restaura todos los campos del centro
  - `ObtenerDescripcion()` - Descripción del estado del centro médico
  - Propiedades: `FechaCreacion`, `UsuarioResponsable`

#### Caretaker (Gestión de Mementos)
- **HistorialActualizaciones.cs**
  - Constructor: `(int limiteHistorial = 50)`
  - `GuardarEstado(object entidad, IMemento memento)` - Guarda un estado
  - `ObtenerUltimoEstado(object entidad)` - Obtiene el último estado guardado
  - `PuedeDeshacer(object entidad)` - Verifica si se puede deshacer
  - `DeshacerUltimoCambio(object entidad)` - Deshace el último cambio
  - `ObtenerHistorialCompleto()` - Obtiene todo el historial
  - `ObtenerHistorialEntidad(object entidad)` - Historial de una entidad específica
  - `LimpiarHistorial()` - Limpia todo el historial
  - `LimpiarHistorialEntidad(object entidad)` - Limpia historial de una entidad

#### Originator (Creación y Restauración)
- **ActualizarConMemento<T, TCambio>.cs**
  - Constructor: `(IActualizar<T, TCambio> actualizadorBase, HistorialActualizaciones historial, string usuarioActual)`
  - `Actualizar(T entidad, TCambio cambio)` - Actualiza guardando estado primero
  - `PuedeDeshacer(T entidad)` - Verifica si se puede deshacer
  - `DeshacerUltimoCambio(T entidad)` - Deshace el último cambio
  - `CrearMemento(T entidad)` - Crea el memento apropiado según el tipo

#### Servicio Integrado
- **ActualizarSeguroService<T, TCambio>.cs**
  - Constructor: `(IActualizar<T, TCambio> actualizadorBase, HistorialActualizaciones historial, Perfil? usuarioActual)`
  - `Actualizar(T entidad, TCambio cambio)` - Actualización con validaciones y memento
  - `PuedeDeshacer(T entidad)` - Verifica capacidad de deshacer
  - `DeshacerUltimoCambio(T entidad)` - Deshace cambios
  - `ActualizarConResultado(T entidad, TCambio cambio)` - Actualización con resultado detallado
  - `TienePermisos(T entidad)` - Validación simple de permisos:
    - Admin: puede actualizar todo
    - Operador: solo alertas
    - Paramédico: solo alertas asignadas a él

#### Clase de Resultado
- **ResultadoActualizacion<T>.cs**
  - `Exitoso { get; set; }` - Si la actualización fue exitosa
  - `Entidad { get; set; }` - La entidad actualizada
  - `Mensaje { get; set; }` - Mensaje descriptivo del resultado
  - `PuedeDeshacer { get; set; }` - Si se puede deshacer esta actualización
  - `FechaActualizacion { get; set; }` - Cuándo se realizó la actualización

#### Inyecciones
- **ActualizadorAdmin.cs**
  - `ActualizarCentroMedico { get; }` - Para actualizar centros médicos
  - `ActualizarCuenta { get; }` - Para actualizar cuentas
  - `ActualizarAlerta { get; }` - Para actualizar alertas
  - `ActualizarEstadoAlerta { get; }` - Para cambiar estado de alertas
  - `ActualizarNivelTriaje { get; }` - Para cambiar nivel de triaje
  - `ActualizarEstadoCuenta { get; }` - Para activar/desactivar cuentas

### Propósito
Guardar y restaurar estados anteriores de entidades médicas críticas (alertas, centros médicos) para permitir reversión de cambios importantes en el sistema hospitalario.

---

## ARQUITECTURA GENERAL DEL SISTEMA

### Distribución por Funcionalidad

| Funcionalidad | Patrones Implementados | Total Archivos |
|---------------|------------------------|----------------|
| **Crear** | Factory Method + Observer | 8 archivos |
| **Mostrar** | Decorator + Dependency Injection | 10 archivos |
| **Actualizar** | Memento | 7 archivos |
| **Comunicacion** | Observer + Dependency Injection | 6 archivos |
| **Eliminar** | Dependency Injection básico | 2 archivos inyección |
| **Asignar** | Dependency Injection básico | 2 archivos inyección |
| **Sistema Global** | Singleton | 1 archivo |

### Estadísticas de Implementación

**Total de Patrones GoF**: 5 de 23
- **Creacionales**: Singleton, Factory Method
- **Estructurales**: Decorator  
- **Comportamentales**: Observer (doble), Memento

**Total de Interfaces**: 6 interfaces principales
**Total de Clases de Implementación**: 19 clases
**Total de Clases de Inyección**: 7 clases

### Características del Sistema Médico

**Seguridad**:
- Validación de permisos por rol de usuario
- Historial completo de cambios con responsables
- Reversión de actualizaciones críticas

**Flexibilidad**:
- Múltiples formatos de visualización
- Notificaciones automáticas de eventos
- Creación estandarizada de perfiles

**Escalabilidad**:
- Arquitectura desacoplada con inyección de dependencias
- Patrones extensibles para nuevos tipos de entidades
- Separación clara de responsabilidades

**Aplicación Médica**:
- Sistema de alertas con niveles de triaje
- Gestión de centros médicos y equipos
- Control de acceso basado en roles médicos
- Auditoría completa para cumplimiento normativo