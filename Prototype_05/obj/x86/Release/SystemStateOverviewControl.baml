   M S B A M L   `   `   `  ���� G  CPrototype_05, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Prototype_05Prototype_05  -    'Prototype_05.SystemStateOverviewControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Prototype_05     -     >�Name$  UserControl��$	��645��$	��302��.��� -    �Name$ MainGrid�� � ColumnDefinitions �� $	��100 ��� �� $	��155 � � RowDefinitions �� $	��100 ��� $��12 ��� �� $��52 �.+�� -   $ NavigationGrid��$��2��$��0��$	��100��$��80��$	��Top=� �� �� .+��� -    ��Name$ LastActions��$��0��    InstanceNavigation_Rectangle#���   $	��100������  ! HistoryNavigationColorStrong0 �� Color# �  �� -   $ 	ErrorRect��$��1��$��85��$��	0,-1,-1,0q�#���   ����   HistoryNavigationColorFade1# �  �� -    ��Name$ LastActionsText��$��0��$��Last Actions��$��5,0,0,10q� # TextBox_InstanceNavigationTitle#���  �� -   $ 	ErrorText��$��1��$��
Errors (-)��#���  � -   $ SystemFactsGrid��$��0��$��1�� �� �� �� ��  �� $��0.07* ��� $��0.23* ��� $
��0.2* ��� $��0.25* �.+��� -	   $! RunningInstancesAmountTitle��$��0��$��0��$��2��$��Running Instances��   SystemFactsTitleTextBoxStyle#���  �� -
   $# ExecutingInstancesAmountTitle��$��1��$��1��$��	executing��#���  �� -   $  PausedInstancesAmountTitle��$��2��$��1��$��paused / waiting��#���  �� -   $  FaultyInstancesAmountTitle��$��3��$��1��$��faulty��#���  �� -   $ RunningInstancesAmount��$��0��$��11�� ! SystemFactsAmountTextBoxStyle#���  �� -   $ ExecutingInstancesAmount��$��1��$��9��#���  �� -   $ PausedInstancesAmount��$��2��$��2��#���  �� -   $ FaultyInstancesAmount��$��3��$��0��$
��Left=�#���  �� 	 �� X1$ 20��	 �� Y1$ 25��		 �� X2$	 20��	
 �� Y2$

 89.5��  SystemFacts_ModelLine#���  �� $ 20��$
 37.5��$	 36��$

 37.5��#���  �� $ 20��$
 62.5��$	 36��$

 62.5��#���  �� $ 20��$
 89.5��$	 36��$

 89.5��#���  �� -   $ 	TimeValue��$��1��$��3��$��12:35��$	��100��#���  $��Right=��� -   $ 	DateValue��$��0��$��3��$��Thu, 27th June 2013��$
��Auto��#���  $��Right=��� -   $ MessageRectangle0��$��2��$��1��
�������$��46��$	��Top=�  LogStripLight_Brush#���  �� -   $ MessageRectangle1��$��2��$��1��
�������$��46��$��Center=�  LogStripDark_Brush#���  �� -   $ MessageRectangle2��$��2��$��1��
�������$��46��$��Bottom=�#���  � -   $ LogGrid��$��2��$��1��$	��138��$	��390��$
��Left=�$	��Top=�$y�Visible=� �� �� ��  �� $��50 ��� �� $��75 �.+��� -   $ LogTime0��$��0��$��0��$��11:47�� �� TextAlignment$ Center=� 	 LogTextBoxStyle#��� 	 �� -   $ LogType0��$��0��$��1��$��Finished��#��� 	 �� -   $ LogProcess0��$��0��$��1��$'��!Bring today's Mail to Living Room��#��� 	 $��Bottom=�$��0,0,0,5q��� -   $ LogTime1��$��1��$��0��$��11:44��$ Center=�#��� 	 �� -   $ LogType1��$��1��$��1��$��Started��#��� 	 �� -   $ LogProcess1��$��1��$��1��$��Simulate Presence��#��� 	 $��Bottom=�$��0,0,0,5q��� -   $ LogTime2��$��2��$��0��$��11.42��$ Center=�#��� 	 �� -   $ LogType2��$��2��$��1��$��Started��#��� 	 �� -   $ LogProcess2��$��2��$��1��$'��!Bring today's Mail to Living Room��#��� 	 $��Bottom=�$��0,0,0,5q�� -    $ 	ErrorGrid��$��2��$��1��$	��138��$	��390��$
��Left=�$	��Top=�$y�Hidden=�.+�� -!   $ ErrorMessageGrid��$��1��$��1��$	��138��$	��375��$
��Left=�$	��Top=� �� �� ��  �� $��50 ��� $	��325 �.+��� -"   $ 
ErrorTime0��$��0��$��0��$��11.01��$ Center=�#��� 	 �� -#   $ 
ErrorType0��$��0��$��1��$��Routing Error��#��� 	 �� -$   $ ErrorProcess0��$��0��$��1��$��
Bring post��#��� 	 $��Bottom=�$��0,0,0,5q��� -%   $ 	OKMessage��$ ��System is working properly��$��13(� 
 TextBox_Simple#��� 
 $y�Hidden=�$��Center=�$��Center=�$��0,0,0,20q�$ Center=���Z� �� -&   $ 
OKMessage2��$"��Currently no errors detected��$��13(�#��� 
 $y�Hidden=�$��Center=�$��Center=�$ Center=�$��0,20,0,0q���Z� 