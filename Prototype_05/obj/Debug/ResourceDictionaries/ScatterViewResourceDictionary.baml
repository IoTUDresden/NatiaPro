   M S B A M L   `   `   `  ����m  iMicrosoft.Surface.Presentation.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�dclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic&Microsoft.Surface.Presentation.Generic  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35    StartInstanceSpecialShapeSVI  �� 
TargetType=   7Microsoft.Surface.Presentation.Controls.ScatterViewItem �� BasedOn �� Property �� Value �� 
TargetType �Name J� RenderTransformOrigin �� ScaleX �� ScaleY	 J� Clip
 �� Path � 	Resources  Key g� 	BeginTime g� Duration �� 
TargetName �� TargetProperty W� From	 W� To �� Triggers ]� 
ConditionsG  AMicrosoft.Surface.Presentation.Controls.ScatterContentControlBase �� Property �� Value N� EnterActions ��Name  ActivationTimeline  Main_SpecialSVIShape �� Mode �� RelativeSource  InstanceOption_SV9  3Microsoft.Surface.Presentation.Controls.ScatterView  InstanceOption_SVI�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      6Custom)http://schemas.microsoft.com/surface/2008  5      �&Microsoft_Surface_Presentation_Genericdclr-namespace:Microsoft.Surface.Presentation.Generic;assembly=Microsoft.Surface.Presentation.Generic  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      1System&clr-namespace:System;assembly=mscorlib  5      %�
  &	        7  7  &	 �    &	 q    &	 c    �� 5      "    61   8   .�5   
   ��  � �� 5      �� 5      "   6"   .R�5      � $ 	container��6   # z 6.   z�5      �� 5      $ 1Q�6-   $ 1Q�68   .+�5      �� #��z��5   !   #��z��5   !   #��z��5   !   	 5    !   �� $
 	ClipShape�5!       5&      ���� 5(      ActivationTimeline .��5*      W� $ 00:00:00X�5+   %   $ 00:00:00.50D�6:   $ 	container��5,   %   $ Opacity�5-   %   $ 0X�5.   %   $ 1X�5/   %   50      �� 51      DeactivationTimeline .��52      W� $ 00:00:00X�53   %   $ 00:00:00.50D�6:   $ 	container��54   %   $ Opacity�55   %   $ 1X�56   %   $ 0X�57   %   58      59       5@      ]� 5B       5C      �� 5D   "    �@ ShowsActivationEffects . 6N    5F      �� 5G   "   $( "ActivationTimeline_BeginStoryboard��5H   )   8�� 5I   )   5K      5O      5Q      �� 5R   
    �@ CanMove .  5S      �� 5T   
    �@ CanScale .  5U      �� 5V   
    � ��$ 15��5W      �� 5X   
    � ��$ 15��5Y      �� 5Z   
    � ��$	 100��5[      �� 5\   
    �@ Center$ 60,60&�5]      5^      �� 5a      "    6)   .�5b   
   ��  � �� 6%   �� *Content.MinWidth+$ OneWay=� �� *Self+�� 5c   
    � �� 6&   �� *Content.MinHeight+$ OneWay=� �� *Self+�� 5d   
    � �� 6%   �� *Content.MaxWidth+$ OneWay=� �� *Self+�� 5e   
    � �� 6&   �� *Content.MaxHeight+$ OneWay=� �� *Self+�� 5f   
    � �� 6"   �� *Content.Width+$ TwoWay=� �� *Self+�� 5g   
    � �� 6#   �� *Content.Height+$ TwoWay=� �� *Self+�� 5h   
    � �� 5i      �� 5j      "   6"   .R�5k      �� 5l      5n      �� 5o   
    �@ CanScale .  5p      5q      �� 5s      "   6&   .�5t   
   ��  � ��$ 1��5u      �� 5v   
    � ��$ 1��5w      �� 5x   
    � ��$	 125��5y      �� 5z       � ��$ 0,16,0,0q�5{      �� 5|   
    � ��$	 Top=�5}      5~      �� 5�      "    6'   .�5�   
   ��  �@ Center$ 20,10&�5�      �� 5�   
    �@ Orientation$ 0Q�5�      �� 5�   
    � ��$ 40��5�      �� 5�   
    � ��$ 40��5�      �� 5�   
    � ��$ 40��5�      �� 5�   
    � ��$ 40��5�      �� 5�   
    � ��
 �����5�      �� 5�   
    � ��
 �����5�      �� 5�   
    � ��$ 2q�5�      �� 5�   
    �@ CanScale .  5�      �� 5�   
    �@ 	CanRotate .  5�      5�      5�      