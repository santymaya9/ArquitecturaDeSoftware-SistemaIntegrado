# Implementaci�n del Patr�n Factory Method para Perfiles

## Descripci�n del Patr�n

El **Factory Method** es un patr�n de dise�o creacional que proporciona una interfaz para crear objetos en una superclase, pero permite a las subclases alterar el tipo de objetos que se crear�n. En el contexto del sistema m�dico, este patr�n es ideal para la creaci�n de diferentes tipos de perfiles de usuario.

## Problema que Resuelve

En un sistema m�dico integrado, existen m�ltiples tipos de perfiles (administradores, operadores, param�dicos, pacientes) con diferentes configuraciones y caracter�sticas. Sin Factory Method, tendr�amos:

```csharp
// Creaci�n directa - PROBLEMAS
var admin = new Admin("Ana", "ana@hospital.com", 3001234567, "CC", 12345678, "contrase�a", 1);
var operador = new Operador("Carlos", "carlos@hospital.com", 3009876543, "CC", 87654321, "clave", 2, new List<Alerta>());
var paramedico = new Paramedico(1, "Mar�a", "maria@ambulancias.com", 3004567890, "CC", 56781234, "segura", 3, 5);
```

**Problemas de este enfoque:**
1. **Acoplamiento**: El c�digo cliente est� acoplado a las clases concretas de cada tipo de perfil
2. **Conocimiento detallado**: Se requiere conocer todos los par�metros espec�ficos de cada constructor
3. **Dif�cil migraci�n**: Cambiar de un tipo de perfil a otro implica reescribir el c�digo
4. **Mantenimiento complejo**: Si cambia la implementaci�n de un perfil, hay que modificar todo el c�digo que lo crea

## Soluci�n con Factory Method

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

### 3. Uso del Patr�n

```csharp
// El cliente trabaja con la interfaz, no con implementaciones concretas
IPerfilFactory factory = new ParamedicoFactory();
var paramedico = factory.CrearPerfil(
    "Mar�a L�pez", 
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

## Inyecci�n de Dependencias

El sistema aprovecha el patr�n Factory Method para facilitar la inyecci�n de dependencias:

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

As�, el servicio puede trabajar con cualquier tipo de factory de perfiles sin conocer los detalles espec�ficos.

## Ventajas en el Contexto M�dico

1. **Abstracci�n**: Los servicios que crean cuentas no necesitan conocer los detalles de cada tipo de perfil
2. **Consistencia**: Todos los perfiles se crean siguiendo la misma interfaz
3. **Flexibilidad**: Se pueden cambiar los tipos de perfiles sin modificar el c�digo cliente
4. **Extensibilidad**: A�adir nuevos tipos de perfiles (ej: t�cnicos m�dicos, especialistas) es sencillo
5. **Testabilidad**: Se pueden crear f�cilmente perfiles de prueba con factories simuladas

## Flujo de Trabajo

1. Un servicio recibe una factor�a de perfiles espec�fica mediante inyecci�n de dependencias
2. El servicio utiliza la interfaz gen�rica para crear perfiles sin conocer su tipo concreto
3. La factor�a concreta se encarga de instanciar el tipo adecuado con todos los par�metros necesarios
4. Se pueden intercambiar factor�as para cambiar el comportamiento del sistema

## Conclusi�n

El patr�n Factory Method implementado en el sistema para la creaci�n de perfiles proporciona una soluci�n elegante y flexible para manejar la complejidad de diferentes tipos de usuarios m�dicos, manteniendo el c�digo desacoplado y f�cil de mantener.

Este patr�n es particularmente valioso en un sistema m�dico, donde los diferentes roles (admin, operador, param�dico, paciente) tienen responsabilidades claramente diferenciadas pero siguen compartiendo una interfaz com�n como "Perfil".