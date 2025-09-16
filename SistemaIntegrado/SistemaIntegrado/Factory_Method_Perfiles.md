# Implementación del Patrón Factory Method para Perfiles

## Descripción del Patrón

El **Factory Method** es un patrón de diseño creacional que proporciona una interfaz para crear objetos en una superclase, pero permite a las subclases alterar el tipo de objetos que se crearán. En el contexto del sistema médico, este patrón es ideal para la creación de diferentes tipos de perfiles de usuario.

## Problema que Resuelve

En un sistema médico integrado, existen múltiples tipos de perfiles (administradores, operadores, paramédicos, pacientes) con diferentes configuraciones y características. Sin Factory Method, tendríamos:

```csharp
// Creación directa - PROBLEMAS
var admin = new Admin("Ana", "ana@hospital.com", 3001234567, "CC", 12345678, "contraseña", 1);
var operador = new Operador("Carlos", "carlos@hospital.com", 3009876543, "CC", 87654321, "clave", 2, new List<Alerta>());
var paramedico = new Paramedico(1, "María", "maria@ambulancias.com", 3004567890, "CC", 56781234, "segura", 3, 5);
```

**Problemas de este enfoque:**
1. **Acoplamiento**: El código cliente está acoplado a las clases concretas de cada tipo de perfil
2. **Conocimiento detallado**: Se requiere conocer todos los parámetros específicos de cada constructor
3. **Difícil migración**: Cambiar de un tipo de perfil a otro implica reescribir el código
4. **Mantenimiento complejo**: Si cambia la implementación de un perfil, hay que modificar todo el código que lo crea

## Solución con Factory Method

### 1. Interfaz Factory (Creador Abstracto)

```csharp
public interface IPerfilFactory
{
    Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras);
}
```

### 2. Implementaciones Concretas

```csharp
public class AdminFactory : IPerfilFactory
{
    public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)
    {
        // Para Admin: extras[0] = int numAdmin
        int numAdmin = extras.Length > 0 && extras[0] is int ? (int)extras[0] : 1;
        
        return new Admin(nombre, correo, (int)celular, tipoCedula, (int)cedula, contrasena, numAdmin);
    }
}

public class OperadorFactory : IPerfilFactory
{
    public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)
    {
        // Para Operador: extras[0] = int numOperador, extras[1] = List<Alerta> alertas
        int numOperador = extras.Length > 0 && extras[0] is int ? (int)extras[0] : 1;
        List<Alerta> alertas = extras.Length > 1 && extras[1] is List<Alerta> ? (List<Alerta>)extras[1] : new List<Alerta>();
        
        return new Operador(nombre, correo, (int)celular, tipoCedula, (int)cedula, contrasena, numOperador, alertas);
    }
}

// Y otras implementaciones...
```

### 3. Uso del Patrón

```csharp
// El cliente trabaja con la interfaz, no con implementaciones concretas
IPerfilFactory factory = new ParamedicoFactory();
var paramedico = factory.CrearPerfil(
    "María López", 
    "maria@ambulancias.com", 
    "CC", 
    56781234, 
    3004567890, 
    "segura789",
    (uint)1, // id
    3, // numParamedico
    5 // limiteAlertas
);
```

## Inyección de Dependencias

El sistema aprovecha el patrón Factory Method para facilitar la inyección de dependencias:

```csharp
public class CrearCuentaService
{
    private readonly IPerfilFactory perfilFactory;

    public CrearCuentaService(IPerfilFactory factory)
    {
        perfilFactory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
}
```

Así, el servicio puede trabajar con cualquier tipo de factory de perfiles sin conocer los detalles específicos.

## Ventajas en el Contexto Médico

1. **Abstracción**: Los servicios que crean cuentas no necesitan conocer los detalles de cada tipo de perfil
2. **Consistencia**: Todos los perfiles se crean siguiendo la misma interfaz
3. **Flexibilidad**: Se pueden cambiar los tipos de perfiles sin modificar el código cliente
4. **Extensibilidad**: Añadir nuevos tipos de perfiles (ej: técnicos médicos, especialistas) es sencillo
5. **Testabilidad**: Se pueden crear fácilmente perfiles de prueba con factories simuladas

## Flujo de Trabajo

1. Un servicio recibe una factoría de perfiles específica mediante inyección de dependencias
2. El servicio utiliza la interfaz genérica para crear perfiles sin conocer su tipo concreto
3. La factoría concreta se encarga de instanciar el tipo adecuado con todos los parámetros necesarios
4. Se pueden intercambiar factorías para cambiar el comportamiento del sistema

## Conclusión

El patrón Factory Method implementado en el sistema para la creación de perfiles proporciona una solución elegante y flexible para manejar la complejidad de diferentes tipos de usuarios médicos, manteniendo el código desacoplado y fácil de mantener.

Este patrón es particularmente valioso en un sistema médico, donde los diferentes roles (admin, operador, paramédico, paciente) tienen responsabilidades claramente diferenciadas pero siguen compartiendo una interfaz común como "Perfil".