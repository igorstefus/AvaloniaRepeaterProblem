There seems to be a problem with bring in to view calls for repeater items. This issue is related to repeater virtualization where item needs to be created and scrollbar position needs to be calculated before item can be brought in to view.

For this to work GetOrCreateElement was made public but usage of it is not relay user friendly and can cause some issues. 
After call to GetOrCreateElement is made user will get new item but calling BringInToView will result in empty repeater viewport if user does not wait for layout pass to be finished. 
This not only mean user is not quite sure when can BringInToView be called but also additional items can be added to repeaters source collection before layout pass is over and this can also lead to empty viewport.

So only option user has is to force layout update after GetOrCreateElement to make sure all is ready for BringInToView call.
I wonder if we can make BringInToView safer feature to use as I imagine bringing things in to view in the repeater is one of things users will need.  
