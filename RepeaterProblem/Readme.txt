Under certain condition ListBox is crashing when filtering items using ReactiveUI.
This is code used to do the filtering of the SourceList bound to items:

IObservable<Func<string, bool>> filter = this.WhenAnyValue(vm => vm.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .Select(BuildFilter);

 _allAvailableItems.Connect()
                .Filter(filter)
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _availableItems)
                .Subscribe();

It seems like it is related to ScrollIntoView call in ItemVirtualizerSimple class. This only happens if you try to filter out already selected item and ScrollViewer already scrolled to it. This is the error:

Unhandled Exception: System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index
   at System.Collections.Generic.List`1.get_Item(Int32 index)
   at System.Collections.ObjectModel.ReadOnlyCollection`1.System.Collections.IList.get_Item(Int32 index)
   at Avalonia.Controls.Presenters.ItemVirtualizerSimple.RecycleContainersForMove(Int32 delta) in D:\a\1\s\src\Avalonia.Controls\Presenters\ItemVirtualizerSimple.cs:line 436
   at Avalonia.Controls.Presenters.ItemVirtualizerSimple.set_OffsetValue(Double value) in D:\a\1\s\src\Avalonia.Controls\Presenters\ItemVirtualizerSimple.cs:line 100
   at Avalonia.Controls.Presenters.ItemVirtualizerSimple.ScrollIntoView(Int32 index) in D:\a\1\s\src\Avalonia.Controls\Presenters\ItemVirtualizerSimple.cs:line 539
   at Avalonia.Controls.Presenters.ItemVirtualizerSimple.ScrollIntoView(Object item) in D:\a\1\s\src\Avalonia.Controls\Presenters\ItemVirtualizerSimple.cs:line 307
   at Avalonia.Controls.Primitives.SelectingItemsControl.UpdateSelectedItem(Int32 index, Boolean clear) in D:\a\1\s\src\Avalonia.Controls\Primitives\SelectingItemsControl.cs:line 1054
   at Avalonia.Controls.Primitives.SelectingItemsControl.ItemsCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e) in D:\a\1\s\src\Avalonia.Controls\Primitives\SelectingItemsControl.cs:line 344
   at Avalonia.Reactive.LightweightObservableBase`1.PublishNext(T value) in D:\a\1\s\src\Avalonia.Base\Reactive\LightweightObservableBase.cs:line 132
   at Avalonia.Utilities.WeakSubscriptionManager.Subscription`1.OnEvent(Object sender, T eventArgs) in D:\a\1\s\src\Avalonia.Base\Utilities\WeakSubscriptionManager.cs:line 187
   at System.Collections.ObjectModel.ReadOnlyObservableCollection`1.OnCollectionChanged(NotifyCollectionChangedEventArgs args)
   at System.Collections.ObjectModel.ReadOnlyObservableCollection`1.HandleCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
   at System.Collections.ObjectModel.ObservableCollection`1.OnCollectionChanged(NotifyCollectionChangedEventArgs e)
   at DynamicData.Binding.ObservableCollectionAdaptor`1.Adapt(IChangeSet`1 changes) in d:\a\1\s\src\DynamicData\Binding\ObservableCollectionAdaptor.cs:line 49
   at DynamicData.ObservableListEx.<>c__DisplayClass21_0`1.<Adapt>b__1(IChangeSet`1 changes) in d:\a\1\s\src\DynamicData\List\ObservableListEx.cs:line 586
   at System.Reactive.Linq.ObservableImpl.Select`2.Selector._.OnNext(TSource value) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Linq\Observable\Select.cs:line 39
--- End of stack trace from previous location where exception was thrown ---
   at System.Reactive.AutoDetachObserver`1.OnErrorCore(Exception exception) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Internal\AutoDetachObserver.cs:line 77
   at System.Reactive.Sink`1.ForwardOnError(Exception error) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Internal\Sink.cs:line 61
   at System.Reactive.Linq.ObservableImpl.Select`2.Selector._.OnNext(TSource value) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Linq\Observable\Select.cs:line 41
   at System.Reactive.Concurrency.Synchronize`1._.OnNext(TSource value) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Concurrency\Synchronization.Synchronize.cs:line 42
   at System.Reactive.ObserveOnObserverNew`1.DrainStep(ConcurrentQueue`1 q) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Internal\ScheduledObserver.cs:line 577
   at System.Reactive.ObserveOnObserverNew`1.DrainShortRunning(IScheduler recursiveScheduler) in D:\a\1\s\Rx.NET\Source\src\System.Reactive\Internal\ScheduledObserver.cs:line 509
   at Avalonia.Threading.AvaloniaScheduler.<>c__DisplayClass2_1`1.<Schedule>b__1() in D:\a\1\s\src\Avalonia.Base\Threading\AvaloniaScheduler.cs:line 40
   at Avalonia.Threading.JobRunner.RunJobs(Nullable`1 priority) in D:\a\1\s\src\Avalonia.Base\Threading\JobRunner.cs:line 40
   at Avalonia.Win32.Win32Platform.WndProc(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam) in D:\a\1\s\src\Windows\Avalonia.Win32\Win32Platform.cs:line 174
   at Avalonia.Win32.Interop.UnmanagedMethods.DispatchMessage(MSG& lpmsg)
   at Avalonia.Win32.Win32Platform.RunLoop(CancellationToken cancellationToken) in D:\a\1\s\src\Windows\Avalonia.Win32\Win32Platform.cs:line 121
   at Avalonia.Threading.Dispatcher.MainLoop(CancellationToken cancellationToken) in D:\a\1\s\src\Avalonia.Base\Threading\Dispatcher.cs:line 65
   at ListProblem.Program.AppMain(Application app, String[] args) in D:\Repos\ListProblem\ListProblem\Program.cs:line 32
   at ListProblem.Program.Main(String[] args) in D:\Repos\ListProblem\ListProblem\Program.cs:line 15