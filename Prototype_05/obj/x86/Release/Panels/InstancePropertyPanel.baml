   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  (    "Prototype_05.InstancePropertyPanel   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  &localclr-namespace:Prototype_05  -     >�Name$  UserControl��$	��665��$	��957��.��� -    �Name$ MainGrid�� � ColumnDefinitions �� $	��120 ��� �� $��30 ��� $	��125 � � RowDefinitions �� $��73 ��� $	��254 ��� $	��430 ��� $	��200 �.+�� -   $ CategoryGrid��$��1��$��3��$	��120��$��40��$	��Top=� �� .+��� -    ��Name$ Category0Rect��$��0��    InstanceDetailNavi_Rectangle#���   �� -    ��Name$ Category0Text��$��0��$��All Instances��  TextBox_InstanceTitle#���  F� $��4��$��1������  ! InstanceDetailBackgroundColor �� Color# �  ��B� ��D� E�  E� IsClosed .  E� 
StartPoint$
 0,27&���@� �� -   "BackgroundPathBottomLeftCorner��	 �� Point$	 0,957&��� -   #BackgroundPathBottomRightCorner��$	 390,957&��� $	 390,27&�� -   $ 
HeaderGrid��$��0��$��1�� �� $��60 ��� $��30 ��� �� $��30 ��� �� $��65 �.+��� -	   $ TitleHeader��  Item_TextBoxHeader_Name#���  $��5��$��	10,27,0,0q�.��Processinstance Name� -
   $ DataGrid��$��1��$��1�� �� $��30 ��� �� $��30 �.+���   Item_TextBox_Title#���  $��0��$��3��.������ $ 	#FF2F1D0E��.��General Informations� -   $ Data_InfoGrid��$��1��$��20,10,20,10q� �� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��0 ��� $��0 ��� $��0 ��� $��0 � �� $��0.55* ��� $��0.45* �.+��� $��0��$��2��$��Related Modelname�� " Item_TextBoxData_AttributeLeft#���  ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  -   
  Name$
 ModelButton��$��1��$��2��$��26��$
��Left=�$��	-10,0,0,0q�$��	10,0,10,0q�$��0q�$��Center=���Z� .���� -   $ 	ModelText��$��Process Model Name�� �� TextDecorations$ 	Underline{�  Item_TextBoxData_ValueLeft#���  �� $��2��$��State��#���  �� -   $ 	StateText��$��3��$��	executing��#���  �� $��2��$��ID�� # Item_TextBoxData_AttributeRight#���  �� -   $ IdText��$��3��$��768-14��  Item_TextBoxData_ValueRight#���  �� $��4��$��
Start Time��#���  �� -   $ StartTimeText��$��5��$��today, 11:00��#���  �� $��4��$��Estimated End Time��#���  �� -   $ EndTimeText��$��5��$��today, 12:27��#���  �� $��6��$��Current Step��#���    -   $
 CurrentStep_Data_Button��$��7��$��2��$��26��$
��Left=�$��	-10,0,0,0q�$��	10,0,10,0q�$��0q�$��Center=���Z� .���� -   $ 	CStepText��$"��Probably very long Step Name��$ 	Underline{�#���  �� $��8��$��
Parameters��#���  �� $��9��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� $��10��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� $��11��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� -   $ ParameterTitle1��$��9��$��Sending Location��$��30,1,1,1q�#���  �� -   $ ParameterTitle2��$��10��$��Amount��$��30,1,1,1q�#���  �� -   $ ParameterTitle3��$��11��$��Amount��$��30,1,1,1q�#���  �� $��8��$��Assigned Value��#���  �� -   $ ParameterValue1��$��9��$��11.00��#���  �� -   $ ParameterValue2��$��10��$��-��#���  �� -   $ ParameterValue3��$��11��$��-��#���    -   $��3�� 	 Item_ExpandReduceButton#��� 	 ����  
 InstanceItem_TouchColor# � 
 F� -    F�Name$ Data_TopArrow��$��2��$��3��
�������$��1�� J� RenderTransformOrigin$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $	 195,10&��� $	 200,15&�F� -   $ Data_BottomArrow��$��2��$��3��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $	 195,15&��� $	 200,20&�� -   $ HistoryGrid��$��2��$��1�� �� $
��Auto �$��30���� �� $
��Auto �$��30��.+��� #���  $��0��$��0�����	#FF2F1D0E.��
Protocol  -   #��� 	 ���� # � 
 F� -   $ History_TopArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $	 195,10&��� $	 200,15&�F� -    $ History_BottomArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $	 195,15&��� $	 200,20&�� -!   $ History_InfoGrid��$��1�� �� $	��120 ��� .+�� $��0��$��20,10q� �� ��  �� $��2* ��� $��4* ��� $��40 �.+��� $��0��$��Current Step��#���    -"   $!
 CurrentStep_Protocol_Button��$��1��$��26��$
��Left=�$��	-10,0,0,0q�$��10,0,5,0q�$��0q�$	��Top=���Z� .���� -#   $ CStepText_P��$"��Probably very long Step Name��$ 	Underline{�$��0,0,10,0q� �� TextWrapping$
 Wrap=�#���    -$   $
 NextStep_Protocol_Button��$��1��$��1��$��26��$
��Left=�$��-5,0,0,0q�$��5,0,10,0q�$��0q�$	��Top=���Z� .���� -%   $ NStepText_P��$��1��$��Name of next Step��$ 	Underline{�#���  �� $��0��$��	Next Step��#���  �� $��2��$��2��$��Progress��#���  $��0,2,0,0q�$	��Top=�� -&    �Name$ ProgressBar��$��2��$��2��$��20�� � Value$ 42Q�  ProgressBarSurfaceStyle#���  $��Bottom=�$	��3,0q�����   ExecutingBarColor# �  A  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer  $��1��$��	10,0,10,0q�$��Disabled=�$��Visible=� 1 -SurfaceScrollViewerStyleWhiteBorderBrownInlay#���  .����  �� Orientation$ 
Horizontal=�$��0q� � 	Resources '   !Prototype_05.InstanceStepProtocol �� 
TargetType �� Property �� Value��%3   '        �� "  .���  � ��$ 0,0,10,0q�.+�  -'     Name$
 ISP0����Z�   -(   $
 ISP1����Z�   -)   $
 ISP2����Z�   -*   $
 ISP3����Z� � -+   $ 	ScaleGrid��$��3��$��1��$��2������ $ 	#FF443428�� �� �� $��30 �.+�� -,   $ 	ModelGrid�� �� $��30 ��� �� $��30 �.+��� #���  $��0��$��0�����	#FF2F1D0E.��Model  --   $
 Model_ExpandReduceButton��$��0q�$��30��#��� 	 ���� # � 
 F� -.   $ Model_TopArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $	 195,10&��� $	 200,15&�F� -/   $ Model_BottomArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $	 195,15&��� $	 200,20&�� -0   $ Model_InfoGrid��$��1�� �� $��28 ��� -1    ��Name$ ImageRow�� �� $��38 ��� -2    ��Name$ ImageColumn���� $��38 �.+��� $y�Hidden=�$��1��$$��../Images/nextinstancestep.png��$ 0.5,0.5&�$	��Top=�$��8,9,12,0q�z�V� .~���  �� ScaleY$ 1Q� �� ScaleX$ -1Q���  �� AngleY$ 0Q� �� AngleX$ 0Q���  �� Angle$ 0Q�T� �� $y�Hidden=�$��1��$��2��$$��../Images/nextinstancestep.png��$ 0.5,0.5&�$	��Top=�$��8,9,12,0q��� $y�Hidden=�$��3��$%��../Images/upperinstancestep.png��$ 0.5,0.5&�$��Center=�$��12,0,0,0q�$��13��$
��Left=��� $y�Hidden=�$��3��$��11(�$!��Upper Process Instance Name��  TextBox_Simple#���  $	��Top=�$��30,4,0,0q����	#FF645A52$
��Bold"��� -3    ��Name$ ModelImg��$��2��$��3��$B��</Prototype_05;component/Images/hierarchy2UpInstanceBlank.png��$��0,27,0,5q��� -4   $ ModelNameText��$��2��$��3��$��Process Instance Name��#���  $
��Left=�$	��Top=�$��21,5,0,0q�  -5   $
 ScaleButton��$��3��$��1��$��0q�$��0q�$��0q�$��30��.��� -6   $ ScaleButton_Grid�� �� �� $��30 �.+��� -7   $ 	ScaleArea��$��2�����	#FF645A52$��0������ *ActualWidth+ �� ElementName$ ScaleButton������ *ActualHeight+$ ScaleButton���� -8   $ 	ScaleText��$��0��$��13(�$��Scale Model��#���  $	 0,0&�$��20��$��90��$��0,0,0,8q�$��Bottom=�$��Center=�.��  � LayoutTransform  �� $	 -90Q�.���� -9   $ ScaleOneWayImg��$��2��$��1��$��11��$��13��$#��../Images/SkalierenEinWeg.png��$��0q�$��Center=�$��Center=��� -:   $ ScaleTwoWayImg��$��2��$��1��$��14��$��18��$(��"../Images/SkalierenZweiWegeAI2.png��$��0q�$��Center=�$��Center=�$y�Hidden=�