from django.db import models
from django.utils import timezone
from django.urls import reverse


class Category(models.Model):
    category = models.CharField(max_length=250)
    slug = models.SlugField()

    class Meta:
        verbose_name = "Категорія"
        verbose_name_plural = "Категорії"

    def __str__(self):
        return self.category


class Article(models.Model):
    title = models.CharField(max_length=250)
    description = models.TextField(blank=True)
    pub_date = models.DateTimeField(default=timezone.now)
    slug = models.SlugField(unique_for_date='pub_date')
    main_page = models.BooleanField(default=False)
    category = models.ForeignKey(
        Category,
        related_name='articles',
        blank=True,
        null=True,
        on_delete=models.CASCADE
    )

    class Meta:
        ordering = ['-pub_date']
        verbose_name = "Стаття"
        verbose_name_plural = "Статті"

    def __str__(self):
        return self.title

    def get_absolute_url(self):
        return reverse('home')


class ArticleImage(models.Model):
    article = models.ForeignKey(
        Article,
        related_name='images',
        on_delete=models.CASCADE
    )
    image = models.ImageField(upload_to='photos/')
    title = models.CharField(max_length=250, blank=True)

    class Meta:
        verbose_name = "Фото"
        verbose_name_plural = "Фото"

    def __str__(self):
        return self.title

    @property
    def filename(self):
        return self.image.name.split('/')[-1]