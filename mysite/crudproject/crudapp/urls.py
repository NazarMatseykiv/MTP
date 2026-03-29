from django.urls import path
from . import views

urlpatterns = [
    path('', views.show_orders, name='show_orders'),
    path('create/', views.create_order, name='create_order'),
    path('update/<int:pk>/', views.update_order, name='update_order'),
    path('delete/<int:pk>/', views.delete_order, name='delete_order'),
]