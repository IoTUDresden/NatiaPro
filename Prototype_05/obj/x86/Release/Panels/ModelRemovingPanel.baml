   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  %    Prototype_05.ModelRemovingPanel   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  &localclr-namespace:Prototype_05  -     >�Name$  UserControl��$	��420��$	��582��.��� -    �Name$ MainGrid�����	#FF1E311C � RowDefinitions �� $��46 ��� $��45 ��� �� $	��120 �.+��� -    ��Name$ TitleHeader��$��0��$��Remove Process Models��$��5��$��0,0,0,0q����	#FF121D11$��10,1,1,1q�$��13(�   Item_TextBoxHeader_Name#���   � -   $ MarkingButtonGrid��$��1��.+��� $��Select��$��11(�  TextBox_Simple#���  $��Center=�$��
275,15,0,0q�;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  $��30��$��40��$��0q�$��	316,0,0,0q�$
��Left=�$��Center=�$��Center=� & "RemoveMarkingButtonControlTemplate#���  $��Bottom=�$��30������  �� Color$ 	#FF415244��.���� $��1��$	��all��$��12(�#���    $��30��$��55��$��0q�$��	355,0,0,0q�$
��Left=�$��Center=�$��Center=�#���  $��Bottom=�$��30������ $ 	#FF415244��.���� $��1��$
��none��$��12(�#���  � -   $ 
ModelsGrid��$��2��$	��400��$��0,10,0,0q� �� $��0 ��� $��40 ��� $	��500 � � ColumnDefinitions �� $	��170 ��� $��* �.+��� $��1��$��0��$��
Categories��$��17,0,0,0q�$��Center=�$��Center=�$��0q�$��12(�#���  � -   $ CategoryGrid��$��2��$	��170��$	��200��$	��Top=� �� �� �� �� �� .+��� -    ��Name$ Category0Rect��$��0��  Category_Rectangle#���  ����   Category0ColorFadeNEW# �  �� -   $ Category1Rect��$��1��$	��170��#���  ����   Category1ColorStrongNEW# �  �� -	   $ Category2Rect��$��2��#���  ����   Category2ColorFadeNEW# �  �� -
   $ Category3Rect��$��3��#���  ����   Category3ColorFadeNEW# �  �� -   $ Category4Rect��$��4��#���  ����   Category4ColorFadeNEW# �  �� -    ��Name$ Category0Image��$��0��$��../Images/wherz.png�� 	 Category_Image#��� 	 �� -   $ Category1Image��$��1��$��../Images/wschloss.png��$��8,4,0,0q�#��� 	 �� -   $ Category2Image��$��20��$��20��$��2��$ ��../Images/wbesenbesser.png��#��� 	 �� -   $ Category3Image��$��20��$��20��$��3��$��../Images/wstuhl.png��#��� 	 �� -   $ Category4Image��$��20��$��20��$��4��$��../Images/wmusik.png��#��� 	 �� -   $ Category0Text��$��0��$��
Favourites�� 
 TextBox_CategoryTitle#��� 
 �� -   $ Category1Text��$��1��$��Safety��$��	27,0,0,10q�#��� 
 �� -   $ Category2Text��$��2��$��Housekeeping��#��� 
 �� -   $ Category3Text��$��3��$��Convenience��#��� 
 �� -   $ Category4Text��$��4��$��Entertainment��#��� 
 �� -    ��Name$ Category0AmountTextB��$��0��  TextBlock_CategoryAmount#���  0 �� $��10(�$��Normal"�.�/ 5�� -   $ Category1AmountTextB��$��1��#���  2 �� $��10(�$��Normal"�.�/ 6�� -   $ Category2AmountTextB��$��2��#���  0 �� $��10(�$��Normal"�.�/ 7�� -   $ Category3AmountTextB��$��3��#���  1 �� $��10(�$��Normal"�.�/ 9�� -   $ Category4AmountTextB��$��4��#���  0 �� $��10(�$��Normal"�.�/ 5�� -   $ BackgroundRectangleHeader��$��1��$��1������ 	 �� EndPoint$	 0.5,1&�
 �� 
StartPoint$
 0.5,0&�.���  � Color$ 	#FF387C32��� $ 	#00387C32�� � Offset$	 0.9Q��� -   $  BackgroundRectangleListBox��$��2��$��1������ $ 	#00387C32���� $��1��$��1�� �� Orientation$ 
Horizontal=���Z� .+��� -    ��Name$ 	NameLabel��$��
Model Name��$��Center=�$��Center=�$��20, 0, 0, 0q�$��12(�$	��160��
��������� -   $ RemoveLabel��$��	Remove It��$��Center=�$��Center=�$��Center=�$
��Left=�$��0q�$��12(� �� TextWrapping$
 Wrap=�$��50��
�������#���  <  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  -     Name$ Cat1ListBox��$��2��$��1��  SurfaceListBox#��[  " SListBoxControlTempl_BG_Transp#���   J� IsHitTestVisible .  .@�@  :Microsoft.Surface.Presentation.Controls.SurfaceListBoxItem  -      Name$ 
removeitem��$��5,0,5,3q�  RemoveItemStyle#���  .���� -!    ��Name$ 
Stackmodel��$	��220��$��42��.+��� $��../Images/geschmiere.png��$	��220��$��42��$��UniformToFill=��� $��Process Model Name��$��Center=� �� TextDecorations$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�  TextBox_SimpleBlack#���  $��42��
���   �$��1q����� *Width+ �� ElementName$ 
Stackmodel���� 
���   �	 �� X1$	 182��	 �� X2$	 182��	 �� Y2$ 42���� $!��../Images/wloeschselect.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�  -"   $ removeitem2��#���  .���� $	��220��$��42�����	#FFCECECE.+��� $"��Some long Process Model Name��$��Center=�$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�#���  $��42��
���   �$��1q����� *Width+$ 
Stackmodel���� 
���   �$	 182��$	 182��$ 42���� $��../Images/wselect2.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�  -#   $ removeitem3��#���  .���� $	��220��$��42�����	#FFCECECE.+��� $��Process Model Name��$��Center=�$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�#���  $��42��
���   �$��1q����� *Width+$ 
Stackmodel���� 
���   �$	 182��$	 182��$ 42���� $��../Images/wselect2.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�  -$   $ removeitem4��#���  .���� $	��220��$��42��.+��� $��../Images/geschmiere.png��$	��220��$��42��$��UniformToFill=��� $'��!Some very long Process Model Name��$��Center=�$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�#���  $��42��
���   �$��1q����� *Width+$ 
Stackmodel���� 
���   �$	 182��$	 182��$ 42���� $!��../Images/wloeschselect.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�  -%   $ removeitem5��#���  .���� $	��220��$��42�����	#FFCECECE.+��� $��Process M Name��$��Center=�$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�#���  $��42��
���   �$��1q����� *Width+$ 
Stackmodel���� 
���   �$	 182��$	 182��$ 42���� $��../Images/wselect2.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�  -&   $ removeitem6��#���  .���� $	��220��$��42�����	#FFCECECE.+��� $ ��Another Process Model Name��$��Center=�$ 	Underline{�$��
0, 0, 0, 0q�$��5,5,50,5q�$
 Wrap=�$��12(�#���  $��42��
���   �$��1q����� *Width+$ 
Stackmodel���� 
���   �$	 182��$	 182��$ 42���� $��../Images/wselect2.png��$��20��$��20��$��
191,11,0,0q�$��Center=�$��Center=�F� -'    F�Name$ Border��$��1��$��2��$��1��$��3��
���������B� ��D� E� -(   BorderPoint0�� E� IsClosed .  E� 
StartPoint$	 0,0&���@� �� -)   BorderPoint1�� �� Point$ 0,328&��� -*   BorderPoint2��$ 230,328&��� -+   BorderPoint3��$ 230,0&�� -,   $ ApproveGrid��$��3��$	��340��$��Center=� �� �� $	��150 � �� $��40 ��� .+��� --   $ OkText��$��0��$��0��$��Remove 3 Models��$��12(�$��10,0,0,0q�#���  $��Center=�$
��Left=�
�������$	��120��$
 Wrap=�$��0q�$��Center=��� -.   $ 
CancelText��$��0��$��1��$#��Cancel: no Model gets removed��$��12(�$��10,0,0,0q�#���  $��Center=�$��Center=�$
��Left=�$
 Wrap=�$	��120���� -/   $ OkRectangle��$��0��  InstanceOption_Rectangle#���  $	��110���� $��0��  InstanceDirection_Image#���  $
��Left=�$��
76,19,10,0q�  -0     Name$ OkButton��$��0��  InstanceOption_Button#���     OptionsButtonControlTemplate#���  .���� -1   $ PausePlayImage��$��../Images/ok_green.png��  InstanceOption_Image#���  �� -2   $ CancelRectangle��$��1��#���  $	��110���� $��1��#���  $
��Left=�$��
76,19,10,0q�  -3   $ CancelButton��$��1��#���  #���  .���� $��../Images/cancel_red2.png��#���  $��30��$��30��