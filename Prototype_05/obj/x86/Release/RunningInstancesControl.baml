   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  *    $Prototype_05.RunningInstancesControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  &localclr-namespace:Prototype_05  -     >�Name$  UserControl��$	��980��$	��865��.��� -    �Name$ MainGrid�� � RowDefinitions �� $��40 ���  � ColumnDefinitions �� $	��140 ��� .+��� -    ��Name$ GroupLabelText��$��0��$��0��$��Group Instances by��$��11(� �� TextWrapping$
 Wrap=�$	��122��$��	3,0,-1,10q�$��Bottom=�$��Right=�   TextBox_Simple#���   � -   $ 	GroupGrid��$��1��$��2��$	��140��$	��200��$	��Top=� �� �� �� �� �� .+��� -    ��Name$ AllRectangle��$��0��   InstanceNavigation_Rectangle#���  $	��125��$��0,0,-1,0q�����   InstanceColorFadeAll �� Color# �  �� -   $ CategoryRectangle��$��1��$��	0,-1,-1,0q�#���  $	��125������   InstanceColorFade0# �  �� -   $ StateRectangle��$��2��$	��140��$��	0,-1,-1,0q�#���  ����   InstanceColor1# �  �� -   $ ProcessTypeRectangle��$��3��$��	0,-1,-1,0q�#���  $	��125������   InstanceColorFade2# �  �� -	   $ StartTimeRectangle��$��4��$��	0,-1,-1,0q�#���  $	��125������   InstanceColorFade3# �  �� -
   $ AllText��$��0��$��Process Model�� # TextBox_InstanceNavigationTitle#���  $	��100���� -   $ CategoryText��$��1��$��Category��#���  $	��100���� -   $ 	StateText��$��2��$��State��$��5,0,0,10q�#���  $	��100���� -   $ ProcessTypeText��$��3��$��Process Type��#���  $	��100���� -   $ StartTimeText��$��4��$��
Start Time��#���  $	��100��A  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer  -     Name$ InstancesSV��$��1��$��1��$	��840��$��Disabled=�$
��Auto=�$
��Left=� / +SurfaceScrollViewerStyleWhiteWithGreyBorder#���  $��1q� 	 InstanceBrush0#��� 	 .���� -   	 ��Name$	 SectionStackPanel��
 �� Orientation$
 Vertical=�$
��Left=�$��1,1,0,0q����	#00C8C8C8.+�� -   $ Section1Grid�� �� $	��200 ��� $	��110 ��� .+��� -   $ Section1Rectangle��$��3��
����������� $ 	#FF3F3F3F���� -   $ Section1Text��$��0��$��Safety��  
 RunningOverviewGroupingTitle#��� 
 �� -   $ Section1AmountText��$��1��$��8 Instances�� ! RunningOverviewGroupingAmount#���  ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  -     Name$ ToggleButton1��  ToggleButton#���    	MiniBrush#���  .��� -   $ ToggleGrid1�� �� �� .+��� -   $ Section1MiniText��$
��Mini��  MiniDetailSliderText#���  �� -   $ Section1DetailText��$��1��$��Detail��{�.  #���    -   $ Cat1SV��$��	10,10,1,0q� % !SurfaceScrollViewerStyleInstances#���  $
��Left=�$��Center=�$��Disabled=�$
��Auto=�.��� -    �Name$ GroupStackPanel1�� � Orientation$ 
Horizontal=�$��0q� � 	Resources '   !Prototype_05.InstanceDetailWidget �� 
TargetType �� Property �� Value%   Prototype_05.InstanceMiniWidget��%f   '        '  )     �� "  .���  � ��$ 5,0,5,10q��� "  .���  � ��$ 5,0,5,10q�� -   $ Section2Grid�� �� $	��200 ��� $	��110 ��� .+��� -   $ Section2Rectangle��$��3��
����������� $ 	#FF353535���� -   $ Section2Text��$��0��$��Housekeeping��#��� 
 �� -   $ Section2AmountText��$��1��$��12 Instances��#���    -   $ ToggleButton2��#���  #���  .��� -    $ ToggleGrid2�� �� �� .+��� -!   $ Section2MiniText��$
��Mini��#���  �� -"   $ Section2DetailText��$��1��$��Detail��{�.  #���    -#   $ Cat2SV��$��	10,10,1,0q�#���  $
��Left=�$��Center=�$��Disabled=�$
��Auto=�.��� -$   $ GroupStackPanel2��$ 
Horizontal=�$��0q� ��%f   '        '  )     �� "  .���  � ��$ 5,0,5,20q��� "  .���  � ��$ 5,0,5,10q�� -%   $ Section3Grid�� �� $	��200 ��� $	��110 ��� .+��� -&   $ Section3Rectangle��$��3��
����������� $ 	#FF232323���� -'   $ Section3Text��$��0��$��Convenience��#��� 
 �� -(   $ Section3AmountText��$��1��$��9 Instances��#���    -)   $ ToggleButton3��#���  .��� -*   $ ToggleGrid3�� �� �� .+��� -+   $ Section3MiniText��$
��Mini��#���  �� -,   $ Section3DetailText��$��1��$��Detail��{�.  #���    --   $ Cat3SV��$��	10,10,1,0q�#���  $
��Left=�$��Center=�$��Disabled=�$
��Auto=�.��� -.   $ GroupStackPanel3��$ 
Horizontal=�$��0q� ��%f   '        '  )     �� "  .���  � ��$ 5,0,5,20q��� "  .���  � ��$ 5,0,5,10q�� -/   $ Section4Grid�� �� $	��200 ��� $	��110 ��� .+��� -0   $ Section4Rectangle��$��3��
����������� $ 	#FF121212���� -1   $ Section4Text��$��0��$��Entertainment��#��� 
 �� -2   $ Section4AmountText��$��1��$��5 Instances��#���    -3   $ ToggleButton4��#���  .��� -4   $ ToggleGrid4�� �� �� .+��� -5   $ Section4MiniText��$
��Mini��#���  �� -6   $ Section4DetailText��$��1��$��Detail��{�.  #���    -7   $ Cat4SV��$��	10,10,0,0q�#���  $
��Left=�$��Center=�$��Disabled=�$
��Auto=�.��� -8   $ GroupStackPanel4��$ 
Horizontal=�$��0q� ��%f   '        '  )     �� "  .���  � ��$ 5,0,5,10q��� "  .���  � ��$ 5,0,5,10q�