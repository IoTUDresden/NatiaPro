   M S B A M L   `   `   `  ���� m  iMicrosoft.Surface.Presentation.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�dclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic&Microsoft.Surface.Presentation.Generic  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 "  SurfaceListBoxItemDataTemplate  �� Orientation ��Name  SurfaceListBox �� 
TargetType<   6Microsoft.Surface.Presentation.Controls.SurfaceListBox �� Property �� Value  SurfaceListBoxItem@  :Microsoft.Surface.Presentation.Controls.SurfaceListBoxItem  ListBoxItem_Brush % !SurfaceListBoxItemControlTemplate  RemoveItemStyle2  ,Microsoft.Surface.Presentation.SurfaceColors$   ListBoxItemBackgroundBrushKey$   ListBoxItemForegroundBrushKey " SListBoxControlTempl_BG_Transp �� 
TargetType J� SnapsToDevicePixelsA  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer	  Name
   
Elasticity �� TypeInTargetAssembly � ColumnDefinitions � RowDefinitions ��Name �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScrollI  CMicrosoft.Surface.Presentation.Controls.Primitives.SurfaceScrollBar  Name4  .System.Windows.Automation.AutomationProperties   AutomationId � LargeChange �� ScrollableWidth � Maximum � Minimum �� Orientation, �� %ComputedHorizontalScrollBarVisibility �� ViewportWidth �� ViewportSize � Value �� Mode �� RelativeSource �� 	Resources  �Name! ]�Name" ]� DecreaseRepeatButtonL  FMicrosoft.Surface.Presentation.Controls.Primitives.SurfaceRepeatButton#  Name$ ��Name% �� Triggers& P� Property' P� Value( �� 
TargetName) ]� IncreaseRepeatButton* ]� ThumbE  ?Microsoft.Surface.Presentation.Controls.Primitives.SurfaceThumb+  Name, � 	Resources  Key- g� Duration	. W� To/ �� TargetProperty0 �� 
TargetName	1 t� To:  4Microsoft.Surface.Presentation.Input.TouchExtensions2 N� EnterActions3 N� ExitActions4 �� BasedOn5 �� Triggers6 �� ScrollableHeight*7 �� #ComputedVerticalScrollBarVisibility8 �� ViewportHeight9 g� 	BeginTime: O� KeyTime; O� Value< J� Opacity= ��Name> �� ContentStringFormat? �� ContentStringFormat ) %SelectedModel_ListBoxItem_BorderBrush #	 SelectedModel_ListBoxItem_Brush@ ]� 
ConditionsA �� PropertyB �� Value 
 ComboListBoxItem " SelectedModelColourListBoxItem ! SelectedModelListBoxItemBrush�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     6Custom)http://schemas.microsoft.com/surface/2008  �&Microsoft_Surface_Presentation_Genericdclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  1System&clr-namespace:System;assembly=mscorlib  % d  &	        &	 �     &	 m    &	     &	 �    &	 �Y    &	
 �^    &	 �c    &	 �c    �� .R��� $  
Horizontal=�.+��� $ 	NameLabel��$��Center=�$��
4, 0, 0, 0q�$��12(�$	��180��
���   ����� *Name+�� $��Center=�$��Right=�$��40, 0, 0, 0q�$��12(�$��40��
���   ����� *CurrentlyExecutedInstances+�� "   .���  � ��$ Hidden=���  � ��$ Disabled=���  � ��$ 0,0,0,5q���  � �� Z� �� "  .���  � ��# �  ��  � ��$ 	10,0,10,0q���  � ��$ 2q���  � ��$ 50����  � ��$ Center=���  � ��# �  �� "  .���  � ��# �  ��  � ��# �  ��  � ��$ 0q���  � ��$ 0q���  � �� �	#00111111��  � ��$	 5,3q���  � ��$ Center=���  � ��# �  �� "   .R�� # z .+�  $	 scrollViewer��#
 z
 #��z��|�.  $
��Auto��#��z��#��z��#��z��#��z��$
��Auto����Z� ���� "  .���  � �� �� "  .R�� $��Stretch=�#��z��# z $��Stretch=���C� *�� "  + �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder��
������ #��z��# z ._��� # z�� .  $��1�� .  #��z��#��z��#��z��$��1��# z   $ PART_HorizontalScrollBar��$ HorizontalScrollBar��#��z��}�. $
��Auto��$ 1Q�# z $ 0Q�$ 
Horizontal=�$��1��# z #y�z # z  �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+���� "   ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track����. "   $# DecreaseRepeat��#��z��$��ScrollBar.PageUpCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$��ScrollBar.PageDownCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$
��Auto��$��Stretch=�$��14����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( 
PART_Track�� � {� .  4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��5 P� & �@��Orientation$' 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track��"   $# DecreaseRepeat��#��z��$��ScrollBar.PageLeftCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$ ��ScrollBar.PageRightCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$��14��$��Stretch=�$
��Auto����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $( DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $( 
PART_Track�� � {� .    $ PART_VerticalScrollBar��$ VerticalScrollBar��#��z��}�. $��1��$ 1Q�# z6 $ 0Q�$ Vertical=�$��0��# z #y�z7 # z8 $
��Auto�� �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+���� "   ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track����. "   $# DecreaseRepeat��#��z��$��ScrollBar.PageUpCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$��ScrollBar.PageDownCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$
��Auto��$��Stretch=�$��14����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( 
PART_Track�� � {� .  4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��5 P� & �@��Orientation$' 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track��"   $# DecreaseRepeat��#��z��$��ScrollBar.PageLeftCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$ ��ScrollBar.PageRightCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$��14��$��Stretch=�$
��Auto����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $( DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $( 
PART_Track�� � {� .  �� $ 	Container��
������ #��z��$��1��$��1��# z % P� & � {�' .  . ��� $( PART_HorizontalScrollBar�� � {� .  �� $( PART_VerticalScrollBar�� � {� .  �� $( PART_ElasticBorder�� C� *�� "  + � ��4 �� "  .���  �@ 
Elasticity$ 0.4,0.4-���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R�� #��z��$��Stretch=�$��Stretch=� �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder��#��z��._��� # z�� .  $��1�� .  #��z��#��z��$��0��$��Disabled=�$��-1q�$��0��$��1��  $ PART_HorizontalScrollBar��$ HorizontalScrollBar��$��0��$��Stretch=�$��Stretch=�$
��Auto��$��Disabled=�$ 1Q�# z $ 0Q�$ 
Horizontal=�$��1��#y�z # z $��Stretch=���Z�  �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+���� "   ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track����. "   $# DecreaseRepeat��#��z��$��ScrollBar.PageUpCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$��ScrollBar.PageDownCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$
��Auto��$��Stretch=�$��14����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( 
PART_Track�� � {� .  4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��5 P� & �@��Orientation$' 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track��"   $# DecreaseRepeat��#��z��$��ScrollBar.PageLeftCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$ ��ScrollBar.PageRightCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$��14��$��Stretch=�$
��Auto����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $( DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $( 
PART_Track�� � {� .    $ PART_VerticalScrollBar��$ VerticalScrollBar��$��1��$��Stretch=�$��Stretch=�$��Disabled=�{�. $ 1Q�# z6 $ 0Q�$ Vertical=�$��0��#y�z7 # z8 $��Stretch=�$
��Auto����Z�  �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+���� "   ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track����. "   $# DecreaseRepeat��#��z��$��ScrollBar.PageUpCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$��ScrollBar.PageDownCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��Center=�$��2����C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$
��Auto��$��Stretch=�$��14����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Width�$0 Thumb��t� $- 0:0:0.05D�$
1 -1,0X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Width�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( 
PART_Track�� � {� .  4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��5 P� & �@��Orientation$' 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $  GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $! 
PART_Track��"   $# DecreaseRepeat��#��z��$��ScrollBar.PageLeftCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��)   $# IncreaseRepeat��#��z��$ ��ScrollBar.PageRightCommand��$��Center=����� "  .���  �@��Interval$	 150����  � �� Z� ��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� #��z��$��40��.+��� $
$ Line��$��2��$��Center=���C� *�� "  +% P� & � {�' .  . ��� $
( Line�� C� *�� "  + � ��*   $+ Thumb��#��z��$��Center=����� "  .���  � �� �� "  , ���� 	Touch .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb���� Release .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��.R�� $
  Grid��#��z��# z .+��� $$ Thumb��$��Stretch=�$��14��$��Stretch=�$
��Auto����C� *�� "  +% P� (& �@  AreAnyInputDevicesCapturedWithin' . 2 �� .���� .��W� $- 0:0:0.05D�$. 16X�$/ Height�$0 Thumb��t� $- 0:0:0.05D�$
1 0,-1X�$/ Margin�$0 Thumb��3 �� .���� .��W� $- 0:0:0.1D�$. 14X�$/ Height�$0 Thumb��t� $- 0:0:0.1D�$1 0X�$/ Margin�$0 Thumb��P� & � {�' .  . ��� $( Thumb�� C� *�� "  + � ��4 �� " ��.���  �@J�SnapsToDevicePixels .  ��  � ��
 ���� ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40��% P� & � {�' .  . ��� $( IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $( DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $( 
PART_Track�� � {� .  % P� & � {�' .  . ��� $( PART_HorizontalScrollBar�� � {� .  �� $( PART_VerticalScrollBar�� � {� .  .���� }�.  #��z��#��z���� $ Border��#��z��#��z��% P� & �@��
IsGrouping' . . ���  � �� .  �� "  , ���� 	Press .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.1��$; 1Q��� Release .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.25��$; 0Q�.R�� $
  Grid��
������ # z .+��� $ 
ButtonBody��#��z��#��z��#��z��# z �� $ PressOverlay��$< 0Q�# z ��C� *�� "  +�� $= Content��#��z��#��z��#? z> #��z��#��z��# z #��z��% P� & �@��
IsSelected' . . ��� $( 
ButtonBody��# �   � ���� $( 
ButtonBody��# � 	  � ���� $( PressOverlay�� C� *�� "  + � ��P� & �@ 	IsPressed' . 2 �� .���� .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.1��; 13 �� .���� .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.25��; 0. ���  C� *�� "  + � ��]� @ �� A � {�B .  �� A �@��
IsSelectedB . ./��� $( 
ButtonBody�� C� *�� "  + � ����  C� *�� "  + � ���� $( Content�� �@J�Opacity$	 0.6Q�]� @ �� A � {�B .  �� A �@��
IsSelectedB .  ./��� $( 
ButtonBody�� C� *�� "  + � ����  C� *�� "  + � ���� $( Content�� �@J�Opacity$
 0.33Q��� "  , ���� 	Press .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.1��$; 1Q��� Release .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.25��$; 0Q�.R�� $
  Grid��
������ # z .+��� $ 
ButtonBody��#��z��#��z��#��z��# z �� $ PressOverlay��$< 0Q�# z ��C� *�� "  +�� $= Content��#��z��#��z��#? z> #��z��#��z��# z #��z��% P� & �@��
IsSelected' . . ��� $( 
ButtonBody�� � �� �	#FF645A52�� $( PressOverlay�� C� *�� "  + � ��P� & �@ 	IsPressed' . 2 �� .���� .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.1��; 13 �� .���� .��U� $9 0X�$/ (UIElement.Opacity)�$0 PressOverlay��.\��� $: 0:0:0.25��; 0. ���  C� *�� "  + � ��]� @ �� A � {�B .  �� A �@��
IsSelectedB . ./��� $( 
ButtonBody�� C� *�� "  + � ����  C� *�� "  + � ���� $( Content�� �@J�Opacity$	 0.6Q�]� @ �� A � {�B .  �� A �@��
IsSelectedB .  ./��� $( 
ButtonBody�� C� *�� "  + � ����  C� *�� "  + � ���� $( Content�� �@J�Opacity$
 0.33Q���	#FFBCD8BC����	#FFBCD8BC��