   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  %    Prototype_05.ModelPropertyPanel   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  &localclr-namespace:Prototype_05  -     >�Name$  UserControl��$	��545��$	��932��.��� -    �Name$ MainGrid�� � ColumnDefinitions �� �� $��30 ��� $	��125 � � RowDefinitions �� $��98 ��� $	��254 ��� $	��380 ��� $	��200 �.+�F� $��4��$��0�����	#FF1E311C��B� ��D� E�  E� IsClosed .  E� 
StartPoint$
 0,51&���@� �� -   "BackgroundPathBottomLeftCorner�� �� Point$ 0,932&��� -   #BackgroundPathBottomRightCorner��$ 390,932&��� $ 390,51&�� -   $ 
HeaderGrid��$y�Visible=�$��0��$��0�� �� �� $��65 �.+��� $y�Hidden=�$��3��$"��../Images/uppermodelstep.png�� J� RenderTransformOrigin$ 0.5,0.5&�$��Bottom=�$��	12,0,0,28q�$��13��$
��Left=��� $y�Hidden=�$��3��$��11(�$��Upper Process Model Name��   TextBox_Simple#���   $��Bottom=�$��	30,0,0,27q����	#FF415244$
��Bold"��� -    ��Name$ TitleHeader��  Item_TextBoxHeader_Name#���  $��5��$��	10,52,0,0q�$��46��.��Processmodel Name� -   $ HeaderGrid2��$y�Hidden=�$��0��$��0�� �� �� $��65 �.+�;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  -   	  Name$	 UpperModelHeaderButton��$��0q�$
��Left=�$��Bottom=�$��21��$��21��$��0,0,0,26q�$��0q�.���� -	   $ UpperModelNameHeader��#���  $��10(�$��	10,0,10,0q�$
��Left=�$��Bottom=����	#FF415244$��21��.��Upper Processstep Name�� -
   $ TitleHeader2��#���  $��	0,52,10,6q�$��25,0,5,0q�$��Bottom=�.��ProcessStep NameF� -   
 F�Name$
 HierarchyLine��$��5��
�������$��1��"�������  �J���� @�� 6n@�	� -   $ DataGrid��$��1��$��0�� �� $��30 ��� �� $��30 �.+���   Item_TextBox_Title#���  $��0��$��3��.��General Informations� -   $ Data_InfoGrid��$��1��$��20,10,20,10q� �� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��2* ��� $��3* ��� $��0 ��� $��0 ��� $��0 ��� $��0 � �� $��0.55* ��� $��0.45* �.+��� $��0��$��2��$
��Name�� " Item_TextBoxData_AttributeLeft#���  �� -   $ 	TitleData��$��1��$��2��$(��"Cook delicious lunch for 3 persons��  Item_TextBoxData_ValueLeft#���  �� $��2��$��ID�� # Item_TextBoxData_AttributeRight#���  �� -   $ Id��$��3��$	��768��  Item_TextBoxData_ValueRight#���  �� $��2��$
��Type��#���  �� -   $
 Type��$��3��$��Clean-Process��#���  �� $��4��$ ��Average Execution Duration��#���  �� -   $ Duration��$��5��$��01h 23'��#���  �� $��4��$��Running Instances��#���    -   $	 InstancesAmountButton��$��5��$��1��$��26��$
��Left=�$��	-10,0,0,0q�$��10,0,0,0q�$��0q�$��Center=���Z� .���� -   $ 	Instances��$��1�� �� TextDecorations$ 	Underline{�#���  �� $��6��$��2��$��Category��#���  �� -   $ 
Categories��$��7��$��2��$��Housekeeping, Favourites��#���  �� $��8��$��
Parameters��#���  �� $��9��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� $��10��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� $��11��$��4,0,0,10q�$
��Left=�$��Bottom=�$��6��$"��../Images/parameterpfeil.png��$
��24.6��$
��Fill=��� -   $ ParameterTitle1��$��9��$��Start at��$��30,1,1,1q�#���  �� -   $ ParameterTitle2��$��10��$��Duration��$��30,1,1,1q�#���  �� -   $ ParameterTitle3��$��11��$��Duration��$��30,1,1,1q�#���  �� $��8��$��Parametertype��#���  �� -   $ ParameterValue1��$��9��$
��Time��#���  �� -   $ ParameterValue2��$��10��$��Timespan��#���  �� -   $ ParameterValue3��$��11��$��Timespan��#���    -   $��3��  Item_ExpandReduceButton#���  F� -   $
 Data_TopArrow��$��2��$��3��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $ 195,10&��� $ 200,15&�F� -   $
 Data_BottomArrow��$��2��$��3��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $ 195,15&��� $ 200,20&�� -   $ HistoryGrid��$��2��$��0�� �� $
��Auto �$��30���� �� $
��Auto �$��30��.+��� #���  $��0��$��0��.��
Protocol  -   #���  F� -    $
 History_TopArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $ 195,10&��� $ 200,15&�F� -!   $
 History_BottomArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $ 195,15&��� $ 200,20&�� -"   $ History_InfoGrid��$��1�� �� $��70 ��� .+��� #���   $
��View��$��11(�$��	28,17,0,0q��� #���   $
��Days��$��12(�$��
62,40,25,0q��� #���   $��Weekdays��$��12(�$��107,40,25,0q��� #���   $��Weeks��$��12(�$��176,40,165,0q��� #���   $��Months��$��12(�$��230,40,100,0q��� #���   $��Years��$��12(�$��	0,40,60,0q�$��Right=�;  5Microsoft.Surface.Presentation.Controls.SurfaceSlider  $	��260�� � Maximum$ 4Q� �� Interval$ 1�� � Value$ 2Q� �� IsSnapToTickEnabled .   SliderStyle#���  $��65,8,65,14.667q���Z� A  ;Microsoft.Surface.Presentation.Controls.SurfaceScrollViewer  $��1��$��	10,0,10,0q�$��Disabled=�$��Visible=� 1	 -SurfaceScrollViewerStyleWhiteBorderGreenInlay#��� 	 .����  �� Orientation$ 
Horizontal=�$��0q� � 	Resources )   #Prototype_05.ModelStatisticProtocol �� 
TargetType �� Property �� Value��%3   '        �� "  .���  � ��$ 0,0,10,0q�.+�  -#     Name$
 MHC1����Z�   -$   $
 MHC2����Z�   -%   $
 MHC3����Z�   -&   $
 MHC4����Z� � -'   $ 	ScaleGrid��$��3��$��2�����	#FF1D301A �� �� $��30 �.+�� -(   $ 	ModelGrid�� �� $��30 ��� �� $��30 �.+��� #���  $��0��$��0��.��Model  -)   $	 Model_ExpandReduceButton��$��0q�$��30��#���  F� -*   $
 Model_TopArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,15&���@� �� $ 195,10&��� $ 200,15&�F� -+   $
 Model_BottomArrow��$��2��
�������$��1��$ 0.5,0.5&���B� ��D� E�  .  $ 190,20&���@� �� $ 195,15&��� $ 200,20&�� -,   $ Model_InfoGrid��$��1�� �� $��28 ��� --    ��Name$ ImageRow�� �� $��38 ��� -.    ��Name$ ImageColumn���� $��38 �.+��� -/    ��Name$ ModelImg��$��2��$��3��$?��9/Prototype_05;component/Images/hierarchy2UpModelBlank.png��$��0,27,0,5q�  -0   $	 Hierarchy0Button��$��1��$��1��$��0q�$y�Hidden=�$��Center=�$��67��$��Center=�$��24����Z� �� -1   $ ModelNameText��$��2��$��3��$��Process Model Name��#���   $
��Left=�$	��Top=�$��21, 5, 0, 0q�  -2   $	 PrevModelHierarchyButton��$y�Hidden=�$��1��$��0��$��0q�$��0q�$��0q�$	��Top=�$��30����Z� .���� -3   $ PrevModelHierarchyImage��$��1��$!��../Images/nextmodelstep.png��$ 0.5,0.5&�$	��Top=�$��8,9,12,0q�z�V� .~���  �� ScaleY$ 1Q� �� ScaleX$ -1Q���  �� AngleY$ 0Q� �� AngleX$ 0Q���  �� Angle$ 0Q�T�   -4   $	 NextModelHierarchyButton��$y�Hidden=�$��1��$��2��$��0q�$��0q�$��0q�$	��Top=�$��30����Z� .���� -5   $ NextModelHierarchyImage��$!��../Images/nextmodelstep.png��$ 0.5,0.5&�$	��Top=�$��8,9,12,0q�  -6   $	 UpperModelHierarchyButton��$y�Hidden=�$��3��$��28��$��28��$
��Left=�$��
10,2,10,10q�$��12,1,1,1q�$��154.2����Z� .��� .+��� -7   $ UpperModelHierarchyImage��$"��../Images/uppermodelstep.png��$ 0.5,0.5&�$��Center=�$��0,3,0,0q�$��13��$
��Left=��� -8   $ UpperModelHierarchyText��$��3��$��11(�$��Upper Process Model Name��#���   $	��Top=�$��18,0,0,0q����	#FF415244$
��Bold"�  -9   $	 ScaleButton��$��3��$��1��$��0q�$��0q�$��0q�$��30��.��� -:   $ ScaleButton_Grid�� �� �� $��30 �.+��� -;    ��Name$ 	ScaleArea��$��2�����	#FF415244$��0������ *ActualWidth+ �� ElementName$ ScaleButton������ *ActualHeight+$ ScaleButton���� -<   $ 	ScaleText��$��0��$��13(�$��Scale Model��#���   $	 0,0&�$��20��$��90��$��0,0,0,8q�$��Bottom=�$��Center=�.��  � LayoutTransform  �� $	 -90Q�.���� -=   $ ScaleOneWayImg��$��2��$��1��$��11��$��13��$#��../Images/SkalierenEinWeg.png��$��0q�$��Center=�$��Center=��� ->   $ ScaleTwoWayImg��$��2��$��1��$��14��$��18��$(��"../Images/SkalierenZweiWegeAI2.png��$��0q�$��Center=�$��Center=�$y�Hidden=�