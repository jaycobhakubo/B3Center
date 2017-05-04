
//Monday, November 18, 2013
//C# WPF Tutorial - Working With The WPF Dispatcher [Intermediate] 



//Proper use of threads can greatly increase the responsiveness of your WPF applications.Unfortunately, you can't update any UI controls from a thread that doesn't own the control. In.NET 2.0, you used Control.Invoke(). Now, we've got something similar but more powerful - the Dispatcher. This tutorial will explain what the Dispatcher is and how to use it.

//First of all, you need to know where the Dispatcher lives.Every Visual (Button, Textbox, Combobox, etc.) inherits from DispacterObject.This object is what allows you to get a hold of the UI thread's Dispatcher.

//The Dispatcher is why you can't directly update controls from outside threads. Anytime you update a Visual, the work is sent through the Dispatcher to the UI thread. The control itself can only be touched by it's owning thread.If you try to do anything with a control from another thread, you'll get a runtime exception. Here's an example that demonstrates this:
//public partial class Window1 : Window
//{
//    public Window1()
//    {
//        InitializeComponent();

//        CheckBox myCheckBox = new CheckBox();
//        myCheckBox.Content = "A Checkbox";

//        System.Threading.Thread thread = new System.Threading.Thread(
//          new System.Threading.ThreadStart(
//            delegate ()
//            {
//                myCheckBox.IsChecked = true;
//            }
//        ));

//        thread.Start();
//    }
//}

//This code creates a Checkbox, then creates a thread which tries to change the checked state.This will fail because the Checkbox was created on a different thread than the one trying to modify it.If you run this code, you'll end up with this exception:
//The calling thread cannot access this object because a different thread owns it.

//So the question is, how do you update the Checkbox from this thread? Fortunately, the Dispatcher gives us the ability to Invoke onto its thread.Invoking probably looks really familiar if you've programmed in .NET 2.0. We actually have an in-depth tutorial on invoking that you might want to read. Below is code using the Dispatcher that will run and update the Checkbox without throwing an exception.
//public Window1()
//{
//    InitializeComponent();

//    CheckBox myCheckBox = new CheckBox();
//    myCheckBox.Content = "A Checkbox";

//    System.Threading.Thread thread = new System.Threading.Thread(
//      new System.Threading.ThreadStart(
//        delegate ()
//        {
//            myCheckBox.Dispatcher.Invoke(
//            System.Windows.Threading.DispatcherPriority.Normal,
//            new Action(
//              delegate ()
//              {
//                  myCheckBox.IsChecked = true;
//              }
//          ));
//        }
//    ));

//    thread.Start();
//}

//Now we've introduced the Dispatcher object. The call to Invoke needs to take a few pieces of information. First is the priority you'd like your work executed with.Next is the delegate that contains the work you actually want to do. If your delegate takes parameters, the Invoke call will also accept an Object or Object[] to pass into the delegate function.It will also accept a timespan that limits the amount of time the Invoke call will wait to execute your code.

//The call to Invoke will block until your function has been executed. Depending on the priority you've set, this might take a while. The Dispatcher also has the ability to invoke code asynchronously using BeginInvoke. Let's look at the same example using BeginInvoke.
//public Window1()
//{
//    InitializeComponent();

//    CheckBox myCheckBox = new CheckBox();
//    myCheckBox.Content = "A Checkbox";

//    System.Threading.Thread thread = new System.Threading.Thread(
//      new System.Threading.ThreadStart(
//        delegate ()
//        {
//            System.Windows.Threading.DispatcherOperation
//            dispatcherOp = myCheckBox.Dispatcher.BeginInvoke(
//            System.Windows.Threading.DispatcherPriority.Normal,
//            new Action(
//              delegate ()
//              {
//                  myCheckBox.IsChecked = true;
//              }
//          ));

//            dispatcherOp.Completed += new EventHandler(dispatcherOp_Completed);
//        }
//    ));

//    thread.Start();
//}

//void dispatcherOp_Completed(object sender, EventArgs e)
//{
//    Console.WriteLine("The checkbox has finished being updated!");
//}

//BeginInvoke takes many of the same arguments as Invoke, but now returns a DispatcherOperation that lets you keep track of the progress of your function.In this case, I simply hooked the Completed event that notifies me when the work has been completed.The DispatcherOperation object also lets you control the Dispatcher by changing its priority or aborting it all together.

//As I mentioned above, we can limit the amount of time an Invoke is allowed to take my passing the Invoke call a TimeSpan structure.Here's an example that will wait 1 second for the queued function to complete:
//public Window1()
//{
//    InitializeComponent();

//    CheckBox myCheckBox = new CheckBox();
//    myCheckBox.Content = "A Checkbox";

//    System.Threading.Thread thread = new System.Threading.Thread(
//      new System.Threading.ThreadStart(
//        delegate ()
//        {
//            myCheckBox.Dispatcher.Invoke(
//            System.Windows.Threading.DispatcherPriority.SystemIdle,
//            TimeSpan.FromSeconds(1),
//            new Action(
//              delegate ()
//              {
//                  myCheckBox.IsChecked = true;
//              }
//          ));
//        }
//    ));

//    thread.Start();
//}

//Unfortunately, there's no way to determine if the invoke finished or timed out from the outside. You can always put code inside the invoked function to determine if it executed.

//All right, so we've got our dispatchers working and code is being executed where it's supposed to be.Invokes, however, are kind of an expensive process. What happens when your controls are being updated from both external threads and the main UI thread.How do you know if you're supposed to use Invoke? The Dispatcher object provides a function that tells you whether or not you have to use Invoke. In WinForms you call InvokeRequired on the actual control. In WPF, you call CheckAccess() on the Dispatcher object. CheckAccess() returns a boolean indicating whether or not you can modify the control without Invoking.
//if (!myCheckBox.Dispatcher.CheckAccess())
//{
//  myCheckBox.Dispatcher.Invoke(
//    System.Windows.Threading.DispatcherPriority.Normal,
//    new Action(
//      delegate()
//      {
//        myCheckBox.IsChecked = true;
//      }
//  ));
//}
//else
//{
//  myCheckBox.IsChecked = true;
//}
 
//So now, before I invoke, I check to see if I even need to.If I do, I invoke the function, if I don't, I simply update the control directly.

//As you can see, the Dispatcher provides a great deal of flexibility over the WinForms Invoke. There's a lot about WPF's dispatching model that we didn't touch in this tutorial. The System.Windows.Threading namespace contains a lot more useful objects that extends the power of the dispatcher even further. 
