# Greenshot 2 Editor

This repository is work in progress, and contains the development of a new Editor for Greenshot 2.x

Major technical changes:

- Greenshot 1.x was build for .NET 2.0 which is no longer supported, this editor is build for .NET 4.5.2
- Greenshot 1.x was using Windows.Forms, this editor will be based on WPF
- Greenshot 1.x used serializing for it's file format, Greenshot 2.x will use googles protobuf.
- Uses MVVM and dependency injection for the UI, making it possible to extend the functionality with additional libraries.
