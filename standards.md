### Naming Conventions
C# Standards Reference: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names  
| Object                                | Convention                |
| -----------------------------         | -----------------         |
| constants (ENUMS included)            | Snake_Case, ALL CAPS      |
| local variables                       | ANY                       |
| public properties                     | camelCase                 |
| private properties (not serialized)   | camelCase, _prefixed      |
| private properties [SerializeField]   | camelCase, _prefixed      |
| [Networked] properties                | camelCase, net_prefixed   |
| public methods                        | PascalCase                |
| private methods                       | PascalCase                |
| method parameters                     | camelCase                 |
| class names                           | PascalCase                |
| ScriptableObjects                     | PascalCase, SObj_prefixed |
<!-- WIP: ANY -->

### Documentation
- Any function / method greater than 3 lines should use /// xml comment documentation.
- All classes should have `<summary></summary>` documentation

### Class Organization
- Standard Class Methods IE Start(), Update() etc are called at the top of the class
- Logic is separated into different parts of the code.
- Variables should be defined as close to the location they are used in as possible.

### WIP
- Retrieve GameManager dependencies via singleton or GameObject or Zenjet dependency injection?  
    - Suggest GameManager with enabled override set