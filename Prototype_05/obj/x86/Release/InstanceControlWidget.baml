   M S B A M L   `   `   `  ����   Prototype_05(    "Prototype_05.InstanceControlWidget   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  -     >�Name$  UserControl��$	��425��$	��315��.��� -    �Name$ MainGrid�� � RowDefinitions �� $��0 ��� $��46 ���  � ColumnDefinitions �� $	��100 ��� .+�� -   $ NavigationGrid��$��1��$��2��$	��100��$��80��$	��Top=� �� �� .+��� -    ��Name$ InstanceRect��$��0��$	��100��    InstanceNavigation_Rectangle#���   ����  �� Color$ 	#FFAF7648���� -   $ AllRect��$��1��$��0,-1,0,0q�#���   ����   InstanceColorFade1# �  �� -    ��Name$ InstanceText��$��0��$��
Instance 7��$��5,0,0,10q� # TextBox_InstanceNavigationTitle#���  �� -   $ AllText��$��1��$��All Instances��#���  �� $��1��$��3��$��1��
�����������  ! InstanceDetailBackgroundColor# �  �� $��1��$��1�����	#FF2F1D0E
�������� -   $ 
HeaderGrid��$��1��$��1��.+��� $��0��$��State��
�������$��2�� ( $Item_TextBoxData_AttributeRightBlack#���  $��Right=�$��Center=�$��
5,-15,15,0q��� -	   $ 	StateText��$��1��$��	executing��$��2�� $  Item_TextBoxData_ValueRightBlack#���  $��Right=�$��Center=�$��	5,15,15,0q�.������ $ White��.��;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  -
   $	��240��$
��Left=�$��10,0,0,0q�$��0q�$��0q�$��Center=���Z� .���� -   $ 	TitleText��$��0��$��Instancename - ID 7��$��14(� �� TextWrapping$
 Wrap=�$��40��  TextBox_Simple#���  $��Center=�$
��Left=� �� TextDecorations$ 	Underline{�� -   $ SolvingGrid��$��2��$��1�� �� $��0 ��� $
��Auto ��� $��30 ��� .+��� -   $ ConfirmText��$��2��$��Confirm selected action:��  ErrorControlOption#���  $
��Auto��$��19,0,0,0q��� -   $ ConfirmRectangle��$��3��  InstanceOption_Rectangle#���  $��	31,16,0,0q�$	��110���� $��3�� 	 InstanceDirection_Image#��� 	 $��-129,19,10,0q�  -   	  Name$	 ConfirmButton��$��3�� 
 InstanceOption_Button#��� 
    OptionsButtonControlTemplate#���  $��38��$��38��$��21,6,0,0q�.���� -   
 ��Name$
 ConfirmMeasureImage��$��Images/pause_black.png��  InstanceOption_Image#���  �� -   $ ChosenMeasureText��$��3��$��Pause this instance��#���  $
��Auto��$��21,-14,21,0q�$��Right=�$��Center=�$	��150��� -   $ SolvingInfoGrid��$
��Auto��$��1��$��	10,10,0,5q� �� $��35 ��� $	��124 � �� $��40 ��� .+��� -   $ 
SelectText��$��0��$��0��$��2��$��11(�$��Select Action��#���  $��11,7,0,0q�$	��Top=�$
��Auto��� -   $ 
SolveiGrid��$��1��$��1��$��0,3,0,0q� �� $
��Auto ��� $
��Auto �.+�� -   $ 
Solve1Grid��$��0��$��1��$��0,0,0,5q� ��  �� $��30 ��� .+��� $��0��$��Images/wnothing.png��  ErrorMeasure_Image#���  �� -   $ DoNothingText��$��0��$#��Don't change Instance's state��#���  � -   $ 
Solve2Grid��$��1��$��1��$��0,5,0,0q� �� �� $��0 ��� ��  �� $��30 ��� .+��� -   $
 PausePlayImage��$��0��$��Images/play_white.png��#���  �� -   $ PausePlayText��$��0��$��Play this instance��#���  �� -   $
 
PauseImage��$��1��$��Images/pause_white.png��#���  �� -   $ 	PauseText��$��1��$��Pause all instances��#���  �� $��2��$��Images/stop_white.png��#���  �� -   $ StopText��$��2��$��Stop this instance��#���  �� $��3��$��Images/wkill.png��#���  �� -   $ KillText��$��3��$��Kill this instance��#���  ;  5Microsoft.Surface.Presentation.Controls.SurfaceSlider  -     Name$ OptionSlider��$��1��$��0��$��2�� � Maximum$ 49Q� �� IsSnapToTickEnabled .  �� Orientation$ Vertical=� % !InstanceControlSurfaceSliderStyle#���   � Value$ 49Q� �� Ticks$ 0,14.5,29,49R�$��-10,-15.04,0,-11q�$
��Left=� � SmallChange$ 0Q�$	��150��  -   $ OptionSlider2��$y�Hidden=�$��1��$��0��$��2��$ 49Q� . $ Vertical=�#���  $ 49Q�$ 0,11.2,22.4,33.6,49R�$��-10,-15,0,-11q�$
��Left=�$ 0Q�$	��176��