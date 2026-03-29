from django.shortcuts import render, redirect, get_object_or_404
from .models import Order
from .forms import OrderForm

def create_order(request):
    if request.method == 'POST':
        form = OrderForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('show_orders')
    else:
        form = OrderForm()
    return render(request, 'crudapp/order.html', {'form': form})

def show_orders(request):
    orders = Order.objects.all()
    return render(request, 'crudapp/show.html', {'orders': orders})

def update_order(request, pk):
    order = get_object_or_404(Order, pk=pk)
    if request.method == 'POST':
        form = OrderForm(request.POST, instance=order)
        if form.is_valid():
            form.save()
            return redirect('show_orders')
    else:
        form = OrderForm(instance=order)
    return render(request, 'crudapp/order.html', {'form': form})

def delete_order(request, pk):
    order = get_object_or_404(Order, pk=pk)
    if request.method == 'POST':
        order.delete()
        return redirect('show_orders')
    return render(request, 'crudapp/confirmation.html', {'order': order})