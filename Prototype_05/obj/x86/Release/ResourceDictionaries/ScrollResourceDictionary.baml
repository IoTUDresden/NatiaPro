   M S B A M L   `   `   `  ���� m  iMicrosoft.Surface.Presentation.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�dclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic&Microsoft.Surface.Presentation.Generic  G CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05 O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35   SliderStyle  �� 
TargetType;   5Microsoft.Surface.Presentation.Controls.SurfaceSlider �� Property �� Value �� 
TargetType � RowDefinitions m�Name m� Fill m� 	Placement ��Name	 �� CornerRadius
 ��Name ]�Name ]� DecreaseRepeatButtonL  FMicrosoft.Surface.Presentation.Controls.Primitives.SurfaceRepeatButton �� DecreaseLarge � OverridesDefaultStyle ]� IncreaseRepeatButton �� IncreaseLarge ]� ThumbE  ?Microsoft.Surface.Presentation.Controls.Primitives.SurfaceThumb  Name  VerticalSliderStyle  SliderStyleWhiteThumb  ControlHitAreaBrush �� Color  SurfaceHitAreaBaseStyle $  SurfaceVertScrollBarRepeatButton2  ,Microsoft.Surface.Presentation.SurfaceColors   TrackBackgroundBrushKey �� Triggers P� Property P� Value �� 
TargetName   ThumbDisabledBrushKey  SurfaceScrollBarThumbStyle �� BasedOn � 	Resources  Key g� Duration �� 
TargetName �� TargetProperty	 W� To	  t� To! �Name" J� SnapsToDevicePixels#   ThumbEnabledBrushKey:  4Microsoft.Surface.Presentation.Input.TouchExtensions$ N� EnterActions 	 Touch% N� ExitActions 	 Release #
 SurfaceScrollBarThumbStyleWhite $  SurfaceHorzScrollBarRepeatButton # SurfaceScrollBarHorizThumbStyle ( $SurfaceScrollBarHorizThumbStyleBrown  SurfaceScrollBarStyleI  CMicrosoft.Surface.Presentation.Controls.Primitives.SurfaceScrollBar& �� 	Resources'  Name( �� ResourceKey) �� Triggers  SurfaceScrollBarStyleBrown 1 -SurfaceScrollBarHorizThumbStyleInstancesWhite ' #SurfaceScrollBarStyleInstancesWhite % !SurfaceScrollViewerStyleInstancesA  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer* � ColumnDefinitions+ �� CanContentScroll, �� CanHorizontallyScroll- �� CanVerticallyScroll.  Name,/ �� %ComputedHorizontalScrollBarVisibility0 �� Orientation1 �� ScrollableWidth2 � Maximum3 � Minimum4  .System.Windows.Automation.AutomationProperties4   AutomationId5 � LargeChange6 �� ViewportWidth7 �� ViewportSize8 � Value9 �� Mode: �� RelativeSource*; �� #ComputedVerticalScrollBarVisibility< �� ScrollableHeight= �� ViewportHeight  SurfaceThumbStyleWhiteQ  KMicrosoft.Surface.Presentation.Controls.TouchVisualizations.TouchVisualizera	  [Microsoft.Surface.Presentation.Controls.TouchVisualizations.RectangleTouchVisualizerAdapter> 	  RadiusX? 	  RadiusY 1 -SurfaceScrollViewerStyleWhiteBorderGreenInlay 1 -SurfaceScrollViewerStyleWhiteBorderBrownInlay / +SurfaceScrollViewerStyleWhiteWithGreyBorder  SliderToolTipStyle @   ToolTipBackgroundBrushKey A   ToolTipForegroundBrushKeyB   ButtonBackgroundBrushKeyC J� Opacity ' #SliderHorzDecreaseRepeatButtonStyle,D   %SliderTrackDecreaseBackgroundBrushKey ' #SliderHorzIncreaseRepeatButtonStyle,E   %SliderTrackIncreaseBackgroundBrushKey  SurfaceThumbStyle  SurfaceSliderHorzThumbStyle ' #SliderVertDecreaseRepeatButtonStyle ' #SliderVertIncreaseRepeatButtonStyle  SurfaceSliderVertThumbStyleF �� RadiusXG �� RadiusY " ErrorMeasureSurfaceSliderStyleH   SliderForegroundBrushKeyI ��NameJ m� ReservedSpaceK m� IsDirectionReversedL   SliderRangeBrushKey .  *InstanceControlSurfaceSliderVertThumbStyle %! !InstanceControlSurfaceSliderStyle (" $SliderHorzDecreaseRepeatButtonStyle2 (# $SliderHorzIncreaseRepeatButtonStyle2 $ SurfaceThumbStyle2  % SurfaceSliderHorzThumbStyle2 (& $SliderVertDecreaseRepeatButtonStyle2 (' $SliderVertIncreaseRepeatButtonStyle2  ( SurfaceSliderVertThumbStyle2 ) NumericValueSliderStyle 
 Prototype_05.CustomTickBarM 
 Name�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     6Custom)http://schemas.microsoft.com/surface/2008  �&Microsoft_Surface_Presentation_Genericdclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic  $localclr-namespace:Prototype_05  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  1System&clr-namespace:System;assembly=mscorlib  %�|  &	        &	     &	 "    &	 �	    &	 �	    7  &	 �
    7  &	 �    7  7  7 	 &	
 !    7  7  7 	 &	 Y    7  &	 �    7  7  7 	 &	 �    7  7  7 	 &	 F    7  7  7 
 0�� $( SurfaceHitAreaBaseStyle��17  7  7  &	 �    7  7  7 
 0�� $( SurfaceHitAreaBaseStyle��17  7  7  &	 �     7  7  7 	 &	 #    7  7  7 
 0�� $( SurfaceHitAreaBaseStyle��17  7  7  &	 P(    7  7  &	 �,    7  7  7  7 	 &	 �/    7  7  &	 "4    7  7  &	 ]8    7  7  &	 �<    &	 �=    7  &	 �>    7  &	 |?    7  7  7  7 	 &	 �B    7  &	 �B    7  7  &	 D    7  7  &	 CE    7  7  7  7 	 &	 II    7  7  7  7  7  7  7  7  7  &	  �T    7  7  7  7 	 &	! �X    7  7  7  7  7  7  7  7   7  &	" �c    7  &	# �d    7  &	$ �e    7  7  7  7 	 &	% �h    7 $ &	& �h    7  7  &	' �i    7  7  &	( �j    7  7  7  7 	 &	) 3n    7  7  7  7 " 7 # 7 % 7 & 7 ' 7 ( 7  �� "    .���  � �� �� "   .R��� #��z��#��z��#��z��._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��$y�	Collapsed=�# z��$	 Top=�$��4��$��0��m� $ 
BottomTick��$y�	Collapsed=�# z��$ Bottom=�$��4��$��0���� $ TrackBackground�����	#FFE7EAEA���	#FFAEB1AF$	��0.2q�$	 1��$	��5,0q�$��Center=�$	��1.0��$��1��._��� $��-6,-1q�.+��� $
 PART_SelectionRange��$y�Hidden=�$	��1.0��#��� ��#��� ��$	��0.5��]� $ 
PART_Track��$��1��   #��Z  . ��.  |�.  ���� "  .R��� 
������    #��Z  . ��.  |�.  ���� "  .R��� 
������    $ Thumb��
���   � . ���� "  .R�A� ���	#FF415244���	#FFFFFFFF$��15��$��30���� "    .���  � �� �� "   .R��� #��z��#��z��#��z��._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��$y�	Collapsed=�# z��$	 Top=�$��4��$��0��m� $ 
BottomTick��$y�	Collapsed=�# z��$ Bottom=�$��4��$��0���� $ TrackBackground�����	#FFE7EAEA���	#FFAEB1AF$	��0.2q�$	 1��$	��5,0q�$��Center=�$	��1.0��$��1��._��� $��-6,-1q�.+��� $
 PART_SelectionRange��$y�Hidden=�$	��1.0��#��� ��#��� ��$	��0.5��]� $ 
PART_Track��$��1��   #��Z  . ��.  |�.  ���� "  .R��� 
������    #��Z  . ��.  |�.  ���� "  .R��� 
������    $ Thumb��
���   � . ���� "  .R�A� ���	#FF415244���	#FFFFFFFF$��15��$��30���� "    .���  � ��$
 Left=���  � ��$ 	260,0,0,0q���  � ��$ 2����  �@�Maximum$ 1Q���  � ��$ 50����  �@��IsMoveToPointEnabled . ��  � �� �� "   .R��� #��z��#��z��#��z��._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��$y�	Collapsed=�# z��$	 Top=�$��4��$��0��m� $ 
BottomTick��$y�	Collapsed=�# z��$ Bottom=�$��4��$��0���� $ TrackBackground�����	#FFE7EAEA���	#FFAEB1AF$	��0.2q�$	 1��$	��5,0q�$��Center=�$	��1.0��$��1��._��� $��-6,-1q�.+��� $
 PART_SelectionRange��$y�Hidden=�$	��1.0��#��� ��#��� ��$	��0.5��]� $ 
PART_Track��$��1��   #��Z  . ��.  |�.  ���� "  .R��� 
������    #��Z  . ��.  |�.  ���� "  .R��� 
������    $ Thumb��
���   � . ���� "  .R��� 
����������	#FFFFFFFF$��20��$��10���� $ 	#00FFFFFF���� "  ��.���  �@J�SnapsToDevicePixels .  ��  � ��8   ��  � �� .  ��  � |� .  ��  �@�FocusVisualStyle Z� ��  � ��$ 40����  � ��$ 40���� "   .���  �@��Interval$	 150����  � �� Z� ��  � ��8   ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� $��40��#��z��.+��� $

 Line��$��Center=�$��2��#���   P�  � {� .  . ��� $
 Line��# �   � ���� "   8   .���  � �� �� "   ���� 	Touch .��W� $ 0:0:0.05D�$ Thumb��$ Width�$ 16X�t� $ 0:0:0.05D�$ Thumb��$ Margin�$  	-1,0,-1,0X��� Release .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 14X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��#��z��#" z" .+��� $
 Thumb��$
��Auto��$��14��$��Stretch=�$��Stretch=�#��� #  P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   8   .���  � �� �� "   ���� 	Touch .��W� $ 0:0:0.05D�$ Thumb��$ Width�$ 16X�t� $ 0:0:0.05D�$ Thumb��$ Margin�$  	-1,0,-1,0X��� Release .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 14X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��#��z��#" z" .+��� $
 Thumb��$
��Auto��$��14��$��Stretch=�$��Stretch=�
������� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   .���  �@��Interval$	 150����  � �� Z� ��  � ��8   ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� .  ��  � �� �� "  .R�� $��40��#��z��.+��� $

 Line��$��Center=�$��2��
������� P�  � {� .  . ��� $
 Line��# �   � ���� "   8   .���  � �� �� "   ���� 	Touch .��W� $ 0:0:0.05D�$ Thumb��$ Height�$ 16X�t� $ 0:0:0.05D�$ Thumb��$ Margin�$  	0,-1,0,-1X��� Release .��W� $ 0:0:0.1D�$ Thumb��$ Height�$ 14X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��#��z��#" z" .+��� $
 Thumb��$
��Auto��$��14��$��Stretch=�$��Stretch=����	#FF415244
�������$��1�� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   8   .���  � �� �� "   ���� 	Touch .��W� $ 0:0:0.05D�$ Thumb��$ Height�$ 16X�t� $ 0:0:0.05D�$ Thumb��$ Margin�$  	0,-1,0,-1X��� Release .��W� $ 0:0:0.1D�$ Thumb��$ Height�$ 14X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��#��z��#" z" .+��� $
 Thumb��$
��Auto��$��14��$��Stretch=�$��Stretch=����	#FF645A52
�������$��1�� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   & ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track����.    $' IncreaseRepeat��#��z��$��Center=�8��  $��ScrollBar.PageDownCommand��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageUpCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ 
PART_Track�� � {� .   2 ) P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageLeftCommand��   $' IncreaseRepeat��#��z��$��Center=�8�� $ ��ScrollBar.PageRightCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $ DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $ 
PART_Track�� � {� .  �� "   & ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track����.    $' IncreaseRepeat��#��z��$��Center=�8��  $��ScrollBar.PageDownCommand��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageUpCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ 
PART_Track�� � {� .   2 ) P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageLeftCommand��   $' IncreaseRepeat��#��z��$��Center=�8�� $ ��ScrollBar.PageRightCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $ DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $ 
PART_Track�� � {� .  �� "   8   .���  � �� �� "   ���� 	Touch .��W� $ 0:0:0.05D�$ Thumb��$ Height�$ 16X�t� $ 0:0:0.05D�$ Thumb��$ Margin�$  	0,-1,0,-1X��� Release .��W� $ 0:0:0.1D�$ Thumb��$ Height�$ 14X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��#��z��#" z" .+��� $
 Thumb��$
��Auto��$��14��$��Stretch=�$��Stretch=�
�������
�������$��1�� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   & ��X�(��       *31SystemParameters.VerticalScrollBarButtonHeightKey+)38Q�X�(��       *42SystemParameters.HorizontalScrollBarButtonWidthKey+)38Q�.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track����.    $' IncreaseRepeat��#��z��$��Center=�8��  $��ScrollBar.PageDownCommand��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageUpCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ 
PART_Track�� � {� .   2 ) P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 38����  � �� �� "  .R�� $! GridRoot��#��z��.+��� $ Track��$��Stretch=�$��Stretch=�._�]� $ 
PART_Track��   $' DecreaseRepeat��#��z��$��Center=�8�� $��ScrollBar.PageLeftCommand��   $' IncreaseRepeat��#��z��$��Center=�8�� $ ��ScrollBar.PageRightCommand��   $ Thumb��8�� #��z��$��Center=� P�  � {� .  . ��� $ IncreaseRepeat�� �@J�Opacity$
 0.33Q��� $ DecreaseRepeat�� �@J�Opacity$
 0.33Q��� $ 
PART_Track�� � {� .  �� "   .���  �@ 
Elasticity$ 0.4,0.4-���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R�� $��Stretch=�$��Stretch=���Z� * �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder��#��z��._��� $��-1,-1,-1,-1q�$��0��$��1��$��0��$��1��#��z��#��z��#+ z��, .  - .  $��Disabled=�  $. PART_HorizontalScrollBar��#y�z/ $��0,0,10,0q�$��0��$��1��$0 
Horizontal=�#2 z1 $3 0Q�$4 HorizontalScrollBar��$
��Auto��8��  $5 1Q�#7 z6 $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�8 �� *HorizontalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�   $. PART_VerticalScrollBar��#y�z; $��1��$��0��$0 Vertical=�#2 z< $3 0Q�$4 VerticalScrollBar��$
��Auto��8�� $5 1Q�#7 z= $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�{�. 8 �� *VerticalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�  P�  � {� .  . ��� $ PART_HorizontalScrollBar�� � {� .  �� $ PART_VerticalScrollBar�� � {� .  �� "   8   .���  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 38X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 16X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  -16,7,-16,0X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ Width�$ 6X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 14X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  0,8,0,0X�.R�� $
! Grid��.+��� $
 HitArea��$��Stretch=�$��Stretch=�$��38��$��38��$��-18,0,-18,0q�8�� �� $
 Thumb��$��0,8,0,0q�$��6��$��14��$��Center=�$	��Top=�
������� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   .���  �@ 
Elasticity$ 0.4,0.4-���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R�� #��z��$��Stretch=�$��Stretch=�* �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder����Z� ._��� $��-1,-1,-1,-1q�$��0��$��1��$��0��$��1��#��z��#��z��#+ z��, .  - .  $��Disabled=�  $. PART_HorizontalScrollBar��#y�z/ $��0��$��1��$0 
Horizontal=�#2 z1 $3 0Q�$4 HorizontalScrollBar��$
��Auto��8��  $5 1Q�#7 z6 $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�8 �� *HorizontalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�   $. PART_VerticalScrollBar��#y�z; $��1��$��0��$0 Vertical=�#2 z< $3 0Q�$4 VerticalScrollBar��$
��Auto��8�� $5 1Q�#7 z= $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�{�. 8 �� *VerticalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�  P�  � {� .  . ��� $ PART_HorizontalScrollBar�� � {� .  �� $ PART_VerticalScrollBar�� � {� .  �� "   .���  �@ 
Elasticity$ 0.4,0.4-���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R�� #��z��$��Stretch=�$��Stretch=�* �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder����Z� ._��� $��-1,-1,-1,-1q�$��0��$��1��$��0��$��1��#��z��#��z��#+ z��, .  - .  $��Disabled=�  $. PART_HorizontalScrollBar��#y�z/ $��0��$��1��$0 
Horizontal=�#2 z1 $3 0Q�$4 HorizontalScrollBar��$
��Auto��8��  $5 1Q�#7 z6 $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�8 �� *HorizontalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�   $. PART_VerticalScrollBar��#y�z; $��1��$��0��$0 Vertical=�#2 z< $3 0Q�$4 VerticalScrollBar��$
��Auto��8�� $5 1Q�#7 z= $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�{�. 8 �� *VerticalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�  P�  � {� .  . ��� $ PART_HorizontalScrollBar�� � {� .  �� $ PART_VerticalScrollBar�� � {� .  �� "   .���  �@ 
Elasticity$ 0.4,0.4-���  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R�� #��z��$��Stretch=�$��Stretch=�* �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ PART_ElasticBorder��
�������$��1q���Z� ._��� $��-1,-1,-1,-1q�$��0��$��1��$��0��$��1��#��z��#��z��#+ z��, .  - .  $��Disabled=�  $. PART_HorizontalScrollBar��#y�z/ $��0��$��1��$0 
Horizontal=�#2 z1 $3 0Q�$4 HorizontalScrollBar��$
��Auto��8��  $5 1Q�#7 z6 $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�8 �� *HorizontalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�   $. PART_VerticalScrollBar��#y�z; $��1��$��0��$0 Vertical=�#2 z< $3 0Q�$4 VerticalScrollBar��$
��Auto��8�� $5 1Q�#7 z= $��Disabled=�$��Stretch=�$��Stretch=�$��Stretch=�{�. 
�������$��1q�8 �� *VerticalOffset+$9 OneWay=�: �� *TemplatedParent+��Z�  P�  � {� .  . ��� $ PART_HorizontalScrollBar�� � {� .  �� $ PART_VerticalScrollBar�� � {� .  �� "  ��.���  � y�$ 	Collapsed=���  � }� .  ��  � ��# � @ ��  � ��# � A ��  � ��$ 5,2,5,3q���  � �� �� " ��.R�� #��� B .+��� $��Stretch=�#��z��#C zC ._��� #��z��#��z��#��z���� "   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8��  $��38��$��38��.+��� $	��Top=�$��1,14,1,0q�$��2��#��� D �� "   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8��  $��38��$��38��.+��� $	��Top=�$��1,14,1,0q�$��2��#��� E �� "   8   .���  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 38X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 16X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  -16,7,-16,0X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ Width�$ 6X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 14X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  0,8,0,0X�.R�� $
! Grid��.+��� $
 HitArea��$��Stretch=�$��Stretch=�$��38��$��38��$��-18,0,-18,0q�8�� �� $
 Thumb��$��0,8,0,0q�$��6��$��14��$��Center=�$	��Top=�#��� #  P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   8   .���  � ��$ 38���� "   8   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$
 Left=���  � �� �� "  .R�� 8�� $��38��$��38��.+��� $
��Left=�$��5,1,0,1q�$��2�����	#FFE7EAEA���	#FFAEB1AF$	��0.2���� "   8   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$
 Left=���  � �� �� "  .R�� 8�� $��38��$��38��.+��� $
��Left=�$��5,1,0,1q�$��2�����	#FFE7EAEA���	#FFAEB1AF$	��0.2���� "   8   .���  �@�OverridesDefaultStyle . ��  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � ��$	 400����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 16X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 30X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,-19,0,-19X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ 14X�$ Width�W� $ 0:0:0.15D�$ Thumb��$ Height�$ 23X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��.+��� $
 HitArea��$
��Left=�$��Center=�$��30��$��38��$��-5,-19,0,-19q�8�� �� $
 SelectionThumb��$F 2��$G 2��$	��295��$��23��$
��Left=�$��Center=����	#20FFFFFF�� $
 Thumb��$F 2��$G 2��$��14��$��30��$
��Left=�$��Center=����#9C84B1
������� P�  � {� .  . ��� $ Thumb��# �   � ��P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� �� "    8   .���  �@��IsFlicksEnabled .  ��  � �� �	#00FFFFFF��  �@��IsMoveToPointEnabled .  ��  � ��# � H ��  � �� �� "   .R�� $! GridRoot��#��z��$��Stretch=�$
��Auto��.+��� }�.  .+��� $I PART_SurfaceToolTip��8�� � $��	16,0,16,0q�$	��Top=�$��28��.+�m� $ TopTick��$y�	Collapsed=�$��4��$	 Top=�# z��$J 0Q�$	��Top=�K .  m� $ 
BottomTick��$y�	Collapsed=�$��4��$ Bottom=�# z��$J 0Q�$��Bottom=�K .  �� $��Center=�$��	-1,-8,0,0q�.+��� $
 PART_SelectionRange��#��� L $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�$��0,-1,0,0q�   $' SliderDecrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z"  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q��� $ 
BottomTick�� �@J�Opacity$
 0.25Q�) P�  �@��Orientation$ Vertical=�. ���  � �� �� "   .R�� $! GridRoot��#��z��$
��Left=�$
��Auto��.+�� $��	0,16,0,16q�$
��Left=�$	��320��.+�m� $ TopTick��$y�	Collapsed=�$��4��$
 Left=�# z��$J 0Q�$
��Left=�K .  m� $ 
BottomTick��$y�	Collapsed=�$��4��$ Right=�# z��$J 0Q�$��Right=�K .  �� $��Center=�$��-8,1,0,0q�.+��� $
 PART_SelectionRange��#��� E $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�   $' SliderDecrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z" �� }�.  .+��� $I PART_SurfaceToolTip��8��  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q��� $ 
BottomTick�� �@J�Opacity$
 0.25Q��� "   8   .���  �@�OverridesDefaultStyle . ��  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � ��$	 400����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 16X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 30X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  0,-19,0,-19X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ 14X�$ Width�W� $ 0:0:0.15D�$ Thumb��$ Height�$ 23X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��.+��� $
 HitArea��$
��Left=�$��Center=�$��30��$��38��$��-5,-19,0,-19q�8�� �� $
 SelectionThumb��$F 2��$G 2��$	��275��$��23��$
��Left=�$��Center=����	#20FFFFFF�� $
 Thumb��$F 2��$G 2��$��14��$��30��$
��Left=�$��Center=����	#FF645A52
������� P�  � {� .  . ��� $ Thumb��# �   � ��P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� �� "    8   .���  �@��IsFlicksEnabled .  ��  � �� �	#00FFFFFF��  �@��IsMoveToPointEnabled .  ��  � ��# � H ��  � �� �� "   .R�� $! GridRoot��#��z��$��Stretch=�$
��Auto��.+��� }�.  .+��� $I PART_SurfaceToolTip��8�� � $��	16,0,16,0q�$	��Top=�$��28��.+�m� $ TopTick��$y�	Collapsed=�$��4��$	 Top=�# z��$J 0Q�$	��Top=�K .  m� $ 
BottomTick��$y�	Collapsed=�$��4��$ Bottom=�# z��$J 0Q�$��Bottom=�K .  �� $��Center=�$��	-1,-8,0,0q�.+��� $
 PART_SelectionRange��#��� L $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�$��0,-1,0,0q�   $' SliderDecrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z"  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q��� $ 
BottomTick�� �@J�Opacity$
 0.25Q�) P�  �@��Orientation$ Vertical=�. ���  � �� �� "   .R�� $! GridRoot��#��z��$
��Left=�$
��Auto��.+�� $��	0,16,0,16q�$
��Left=�$	��320��.+�m� $ TopTick��$y�	Collapsed=�$��4��$
 Left=�# z��$J 0Q�$
��Left=�K .  m� $ 
BottomTick��$y�	Collapsed=�$��4��$ Right=�# z��$J 0Q�$��Right=�K .  �� $��Center=�$��-8,1,0,0q�.+��� $
 PART_SelectionRange��#��� E $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�   $' SliderDecrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z" �� }�.  .+��� $I PART_SurfaceToolTip��8��  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q��� $ 
BottomTick�� �@J�Opacity$
 0.25Q��� "   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8��  $��38��$��38��.+��� $	��Top=�$��1,20,1,0q�$��2��$��6��
��������� "   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8��  $��38��$��38��.+��� $	��Top=�$��1,20,1,0q�$��2��$��6��
��������� "   8   .���  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 16X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ Width�$ 6X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 22X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  	0,12,0,-4X�.R�� $
! Grid��.+��� $
 HitArea��$��Stretch=�$��Stretch=�$��38��$��38��$��-16,4,-16,4q�8�� �� $
 Thumb��$��	0,12,0,-4q�$��6��$��22��$��Center=�$	��Top=�
������� P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� P�  � {� .  . ��� $ Thumb��# �   � ���� "   8   .���  � ��$ 38���� "   8   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8�� $��38��$��38��.+��� $
��Left=�$��12,1,0,1q�$��2��#��� D �� "   8   .���  �@��Interval$	 150����  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "  .R�� 8�� $��38��$��38��.+��� $
��Left=�$��12,1,0,1q�$��2��#��� E �� "   8   .���  �@�OverridesDefaultStyle . ��  �@ Adapter 	  $> 15Q�$? 15Q���  � ��$ 6����  � ��$ 6����  � ��$ 38����  � �� �� "   ���� 	Touch .��W� $ 0:0:0.1D�$ Thumb��$ Width�$ 16X�W� $ 0:0:0.1D�$ Thumb��$ Height�$ 38X�t� $ 0:0:0.1D�$ Thumb��$ Margin�$  -1,-19,-1,-19X��� Release .��W� $ 0:0:0.15D�$ Thumb��$ 14X�$ Width�W� $ 0:0:0.15D�$ Thumb��$ Height�$ 6X�t� $ 0:0:0.15D�$ Thumb��$ Margin�$  0,0,0,0X�.R�� $
! Grid��.+��� $
 HitArea��$��Stretch=�$��Center=�$��38��$��38��$��0,-19,0,-19q�8�� �� $
 Thumb��$��14��$��6��$��Stretch=�$	��Top=�#��� #  P�  � {� .  . ��� $ Thumb��# �   � ��P� ( �@  AreAnyInputDevicesCapturedWithin . $ �� 8�� % �� 8�� �� "    8   .���  �@��IsFlicksEnabled .  ��  � ��8  ��  �@��IsMoveToPointEnabled .  ��  � ��# � H ��  � �� �� "   .R�� $! GridRoot��#��z��$��Stretch=�$
��Auto��.+��� }�.  .+��� $I PART_SurfaceToolTip��8�� � $��	16,0,16,0q�$	��Top=�$��32��.+�
  $M TopTick��$y�	Collapsed=�$��4��$	 Top=�# z��$J 0Q�$	��Top=�K .  
  $M NumericBottomTick��$y�	Collapsed=�$��4��$��-5,0,0,0q�$ Bottom=�
 �����$J 0Q�$��Bottom=�K .  �� $��Center=�$��	-1,-8,0,0q�.+��� $
 PART_SelectionRange��#��� L $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�$��0,-1,0,0q�   $' SliderDecrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	-1,0,-1,0q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z"  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ NumericBottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q�) P�  �@��Orientation$ Vertical=�. ���  � �� �� "   .R�� $! GridRoot��#��z��$��Stretch=�$
��Auto��.+�� $��	0,16,0,16q�$
��Left=�$��28��.+�m� $ TopTick��$y�	Collapsed=�$��4��$
 Left=�# z��$J 0Q�$
��Left=�K .  m� $ 
BottomTick��$y�	Collapsed=�$��4��$ Right=�# z��$J 0Q�$��Right=�K .  �� $��Center=�$��-8,1,0,0q�.+��� $
 PART_SelectionRange��#��� E $��8��$y�Hidden=�]� $ 
PART_Track��$��Center=�   $' SliderDecrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.DecreaseLarge��   $' SliderIncrease��$��	0,-1,0,-1q�#��z��#" z" 8�� $��Slider.IncreaseLarge��   $ Thumb��8�� #��z��#" z" �� }�.  .+��� $I PART_SurfaceToolTip��8��	  P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  � {� .  . ��� $ SliderDecrease�� �@J�Opacity$
 0.25Q��� $ SliderIncrease�� �@J�Opacity$	 0.5Q��� $ TopTick�� �@J�Opacity$
 0.25Q��� $ 
BottomTick�� �@J�Opacity$
 0.25Q�