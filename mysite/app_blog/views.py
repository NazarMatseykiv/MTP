from django.shortcuts import render
from django.views.generic import ListView, DateDetailView
from .models import Article, Category


class HomePageView(ListView):
    model = Category
    template_name = 'home.html'
    context_object_name = 'categories'

    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['articles'] = Article.objects.filter(main_page=True)[:5]
        return context


class ArticleList(ListView):
    model = Article
    template_name = 'articles_list.html'
    context_object_name = 'items'


class ArticleCategoryList(ListView):
    model = Article
    template_name = 'articles_list.html'
    context_object_name = 'items'

    def get_queryset(self):
        return Article.objects.filter(
            category__slug=self.kwargs['slug']
        )

    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        context['category'] = Category.objects.get(
            slug=self.kwargs['slug']
        )
        return context


class ArticleDetail(DateDetailView):
    model = Article
    template_name = 'article_detail.html'
    context_object_name = 'item'
    date_field = 'pub_date'
    month_format = '%m'
    allow_future = True