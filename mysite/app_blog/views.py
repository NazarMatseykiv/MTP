from django.shortcuts import render
from .models import Article


def home(request):
    articles = Article.objects.all()
    return render(request, 'home.html', {'articles': articles})