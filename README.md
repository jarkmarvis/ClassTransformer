# Transformer 

Console app + class library to serialize a class file to json for nuget packaging and deserializing it back to a class file.

Usage: Transformer 'action' 'file'.
Actions: make = serialize and eat = deserialize.
Example: Transformer eat MyClassFile.cs

Class library is available through the "ClassTransformer" namespace. To integrate you need to include only the dll in your project.
