from django.urls import path
from .views import (
    HomePageView,
    ArticleList,
    ArticleCategoryList,
    ArticleDetail
)

urlpatterns = [
    path('', HomePageView.as_view(), name='home'),

    path('articles/', ArticleList.as_view(), name='articles-list'),

    path(
        'articles/category/<slug:slug>/',
        ArticleCategoryList.as_view(),
        name='articles-category-list'
    ),

    path(
        'articles/<year>/<month>/<day>/<slug:slug>/',
        ArticleDetail.as_view(),
        name='article-detail'
    ),
]
from django.conf import settings
from django.conf.urls.static import static

urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)