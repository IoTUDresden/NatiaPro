   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  %    Prototype_05.StartInstancePanel   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  &localclr-namespace:Prototype_05  -     >�Name$  UserControl��$	��300��$��735.5��.��� -    �Name$ MainGrid������  �� Color$ 	#FF453529�� � RowDefinitions �� $��46 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��125 �.+��� -    ��Name$ TitleHeader��$��0��$��Create  new instance of:��$��13(�$��0q�$��27�����	#FF2F1D0E$��10,3,1,5q�$	��Top=�   Item_TextBoxHeader_Name#���   �� -   $ TitleHeader2��$��0��$&�� Probably long Process Model Name��$��13(�$��0q�$��23�����	#FF2F1D0E$��10,1,1,4q�$��Bottom=�#���   � -   $ NumericGrid��$��1��$	��0,5q� �� $
��Auto ��� .+�F� -    F�Name$ NumericPath��$��1��$��2��
�������=���Ѐ����� р�� ��� р�� @�рI@�рI��� � Zb��� 	�� -   $ Numeric1Text��$��0��$��Numeric Parameter��  StartInstanceTextBox#���  �� -   $ SliderValueText��$
��76.4��$��1��#���  $��Right=�$��21,-18,21,0q�$��Center=�;  5Microsoft.Surface.Presentation.Controls.SurfaceSlider  -	     Name$ surfaceSlider��$��1�� � Minimum$ 0Q� � Maximum$	 100Q�	 � Value$
	 76.4Q�
 �� TickPlacement$
 BottomRight=� �� TickFrequency$ 20Q� � LargeChange$ 5Q� � SmallChange$ 1Q�$��9,0,0,5q�$	��225��  NumericValueSliderStyle#���  $
��Left=�� -
   $ NumericGrid2��$��2��$	��0,5q� �� $
��Auto ��� .+�F� $��1��$��2��
�������=���Ѐ����� р�� ��� р�� @�рI@�рI��� � Zb��� 	�� -   $ Numeric2Text��$��0��$��Numeric Parameter2��#���  �� -   $ SliderValueText2��$��3��$��1��#���  $��Right=�$��21,-18,21,0q�$��Center=�  -   $ surfaceSlider2��$��1��$ 1Q�$ 6Q�$	 3Q�$
 BottomRight=�$ 1Q� �� IsSnapToTickEnabled . $ 1Q�$ 1Q�$��9,0,0,5q�$	��225��#���  $
��Left=�� -   $ TimeGrid��$��3��$	��0,5q� �� $
��Auto ��� .+�F� $��1��$��2��
�������=���Ѐ����� р�� ��� р�� ���рI���рI��� � Zb��� 	�� -   $ TimeGridText��$��0��$��Time-based Parameter��#���  �� $��1��$��80��$��59��$��Right=�$��0,0,52,0q�$��../Images/zeit2.png���� $��1��$	��100��$	��100��$
��Left=�$��31,0,0,0q�$��../Images/uhr2.png��9  3Microsoft.Surface.Presentation.Controls.ScatterView  $��1��$	��120��$	��120��$
��Left=�$��21,0,0,0q�.@�=  7Microsoft.Surface.Presentation.Controls.ScatterViewItem  G  AMicrosoft.Surface.Presentation.Controls.ScatterContentControlBase   Orientation$ 18Q�   StartInstanceSpecialShapeSVI#���  $��30��$��Center=�.��� .+��� $��30��$��50��$	��Top=����	#00000000�� $��13��$��50��$��Center=�$��../Images/zeigergr2.png��$	��Top=�$��0q�$
��Fill=�  #���  $ 62Q�$��30��$��Center=�.��� .+��� $��30��$��50��$	��Top=����	#00000000�� $��15��$��30��$��Center=�$��../Images/zeigerkl2.png��$	��Top=�$��0,20,0,0q�� -   $ LocationGrid��$��4��$	��0,5q� �� $
��Auto ��� .+�F� $��1��$��2��
�������=���Ѐ����� р�� ��� р�� �,рI�,рI��� � Zb��� 	�� -   $ LocationGridText��$��0��$��Location Parameter��#���  ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  -   $��1��$	��110��$��38��
������ $
��Left=�$��
21,-2,21,0q�$��Center=�$��Center=�$��Center=�$��0q�.��� $��23��$	��110�� � ColumnDefinitions �� �� $��10 ��� $��10 �.+��� $��Center=�$��23�����	#FF707070$��3�� �� RadiusX$ 2�� �� RadiusY$ 2�����	#FF707070�� $��Center=�$	��108��$��21�����	#FF645A52$��3��$ 2��$ 2������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFFAFAFA�� � Offset$ 0Q�� $ 	#FFB7B2AF��$ 1Q��� -   $ AreaTitleText��$��Center=�$��12(�$��90��$��Choose Area��  TextBox_Simple#���  $��5,1,1,1q�$
��Left=�F� -   $ AreaBoxPath0��$��1��$��1��
�������"����@B  z �@KL  � �@T�  z 	$��Right=�$��0q� � LayoutTransform V� .~���  �� CenterX$	 0.5Q� �� CenterY$	 0.5Q���  �� CenterX$	 0.5Q� �� CenterY$	 0.5Q���  �� Angle$ 0Q� �� CenterX$	 0.5Q� �� CenterY$	 0.5Q�T� F� -   $ AreaBoxPath1��$��1��$��1��
�������"����@B  � �@KL  $� �@T�  � 	$��Right=�$��0q� V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $ 0Q�$	 0.5Q�$	 0.5Q�T�   -   $��1��$	��135��$��38��
������ $��Right=�$��	0,-2,21,0q�$��Center=�$��Center=�$��Center=�$��0q�.��� $��23��$	��135�� �� �� $��10 ��� $��10 �.+��� $��Center=�$��23�����	#FF707070$��3��$ 2��$ 2�����	#FF707070�� $��Center=�$	��133��$��21�����	#FF645A52$��3��$ 2��$ 2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFFAFAFA��$ 0Q�� $ 	#FFB7B2AF��$ 1Q��� -   $ LocationTitleText��$��Center=�$��12(�$	��114��$��Choose Location��#���  $��5,1,1,1q�$
��Left=�F� -   $ LocationBoxPath0��$��1��$��1��
�������"����@B  z �@KL  � �@T�  z 	$��Right=�$��0q� V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $ 0Q�$	 0.5Q�$	 0.5Q�T� F� -   $ LocationBoxPath1��$��1��$��1��
�������"����@B  � �@KL  $� �@T�  � 	$��Right=�$��0q� V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $ 0Q�$	 0.5Q�$	 0.5Q�T� � -   $ 
ObjectGrid��$��5��$	��0,5q� �� $
��Auto ��� .+�F� $��1��$��2��
�������=���Ѐ����� р�� ��� р�� �,рI�,рI��� � Zb��� 	�� -   $ ObjectGridText��$��0��$��Object Parameter��#���    -   $��1��$	��160��$��38��
������ $
��Left=�$��	17,-2,0,0q�$��Center=�$��Center=�$��Center=�$��0q�.��� $��23��$	��150�� �� �� $��10 ��� $��10 �.+��� $��Center=�$��23�����	#FF707070$��3��$ 2��$ 2�����	#FF707070�� $��Center=�$	��148��$��21�����	#FF645A52$��3��$ 2��$ 2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFFAFAFA��$ 0Q�� $ 	#FFB7B2AF��$ 1Q��� -   $ ObjectTitleText��$��Center=�$��12(�$	��130��$��Choose Object��#���  $��5,1,1,1q�F� -   $ ObjectBoxPath0��$��1��$��1��
�������"����@B  z �@KL  � �@T�  z 	$��Right=�$��0q� V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $ 0Q�$	 0.5Q�$	 0.5Q�T� F� -   $ ObjectBoxPath1��$��1��$��1��
�������"����@B  � �@KL  $� �@T�  � 	$��Right=�$��0q� V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $ 0Q�$	 0.5Q�$	 0.5Q�T� � -    $ JustCreateGrid��$��6��$
��0,15q�.+��� $L��FCreate instance, but do not yet start it. Instance gets waiting-state.�� �� TextWrapping$
 Wrap=�$��	53,0,21,0q�$��Center=�#���  =  7Microsoft.Surface.Presentation.Controls.SurfaceCheckBox  -!      Name$  InstanceToWaitBox��! d� 	IsChecked$! FalseY�
�������$
��10,1q�$��
11,2,10,10q� & "CreateInstanceSurfaceCheckBoxStyle#���  $��1q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF9F9F9��$ 0Q�� $ 	#FFB8B3B0��$ 1Q�� -"   $ ApproveGrid��$��7��$	��280��$��Center=�$��0,0,0,19q� �� �� $	��140 � �� $��40 ��� .+��� -#   $ OkText��$��0��$��0��$��Create and start Instance��$��12(�$��10,0,0,0q�#���  $��Center=�$
��Left=�
�������$	��110��$
 Wrap=�$��0q�$��Center=��� -$   $ 
CancelText��$��0��$��1��$$��Cancel, no Instance is created��$��12(�$��10,0,0,0q�#���  $��Center=�$��Center=�$
��Left=�$
 Wrap=�$	��115���� -%   " ��Name$" OkRectangle��$��0��  InstanceOption_Rectangle#���  $	��100���� $��0��  InstanceDirection_Image#���  $
��Left=�$��
66,19,10,0q�  -&   #  Name$# OkButton��$��0��  InstanceOption_Button#���    	 OptionsButtonControlTemplate#��� 	 .���� -'   $ ��Name$$ OkImage��$��../Images/ok_green.png�� 
 InstanceOption_Image#��� 
 �� -(   $" CancelRectangle��$��1��#���  $	��100���� $��1��#���  $
��Left=�$��
66,19,10,0q�  -)   $# CancelButton��$��1��#���  #��� 	 .���� $��../Images/cancel_red2.png��#��� 
 $��30��$��30��<  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  -*   %  Name$% ObjectComboListBox��$��5��$��3��$	��147��$
��Left=�$��	24,62,0,0q�$	��Top=����	#EA000000$��1q�& �� SelectedIndex$& -1��$y�Hidden=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF9F8F8��� $ 	#FFB9B4B1��$ 1Q�.@�@  :Microsoft.Surface.Presentation.Controls.SurfaceListBoxItem  $��30��$��30��$��Center=�$��0q����	#FF707070  ComboListBoxItem#���  $��0q���Z� .���� -+   $ Object0Text��$��Center=�$��12(�$��today's Mail��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -,   $ Object1Text��$��Center=�$��12(�$��Pot of Coffee��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� --   $ Object2Text��$��Center=�$��12(�$��	Telephone��#���  $��Center=�  -.   $% AreaComboListBox��$��4��$��4��$	��108��$
��Left=�$��	22,62,0,0q�$	��Top=����	#EA000000$��1q�$& -1��$y�Hidden=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF9F8F8��� $ 	#FFB9B4B1��$ 1Q�.@�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -/   $ 	Area0Text��$��Center=�$��12(�$��	1st Floor��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -0   $ 	Area1Text��$��Center=�$��12(�$��	2nd Floor��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -1   $ 	Area2Text��$��Center=�$��12(�$��	3rd Floor��#���  $��Center=�  -2   $% LocationComboListBox��$��4��$��4��$	��133��$
��Left=�$��
145,62,0,0q�$	��Top=����	#EA000000$��1q�$& -1��$y�Hidden=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF9F8F8��� $ 	#FFB9B4B1��$ 1Q�.@�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -3   $ Location0Text��$��Center=�$��12(�$��Living Room��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -4   $ Location1Text��$��Center=�$��12(�$��Kitchen��#���  $��Center=�  $��30��$��30��$��Center=�$��0q����	#FF707070#���  $��0q���Z� .���� -5   $ Location2Text��$��Center=�$��12(�$��	Bath Room��#���  $��Center=�