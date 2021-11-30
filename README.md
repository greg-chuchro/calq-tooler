# calq tooler
Calq Tooler is a lightweight library based on [calq options](https://github.com/greg-chuchro/calq-options) that acts as a dynamic CLI.

## Get Started
```csharp
public class Hello {
    public class World {
        // can be invoked via command-line
        // as "world HelloWorld foo"
        // or "world HelloWorld --helloWorld foo"
        public string HelloWorld(string helloWorld) {
            return helloWorld;
        }
    }
    public World world = new();
}
````
```csharp
Tool.Execute(new Hello());
````
