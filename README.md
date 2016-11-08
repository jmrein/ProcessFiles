This library provides an ultra-simple way of writing a simple UI application which takes an input file, 
does some processing on it, and writes the output to another file.

This library provides:

* A basic UI
* A progress bar
* Error handling
* File/folder open options once the file is created.

All you need to do is write the conversion function! 

## How to use ##
1. Create a Windows Forms project
2. Install the NuGet package
3. In the Main method create an app by calling ProcessFilesApp.Create with two callbacks - one to transform the
input, and one to do any cleanup.
4. Set up any options
5. Call the app's Start() method.

And that's it!
See the Demo application for a basic example.
