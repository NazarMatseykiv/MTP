from django.db import models
from django.urls import reverse
from django.utils import timezone


class Category(models.Model):
    category = models.CharField(
        'Категорія',
        max_length=250,
        help_text='Максимум 250 символів'
    )

    slug = models.SlugField('Slug', unique=True)

    objects = models.Manager()

    class Meta:
        verbose_name = 'Категорія'
        verbose_name_plural = 'Категорії'

    def __str__(self):
        return self.category

    def get_absolute_url(self):
        return reverse(
            'articles-category-list',
            kwargs={'slug': self.slug}
        )


class Article(models.Model):
    title = models.CharField(
        'Заголовок',
        max_length=250,
        help_text='Максимум 250 символів'
    )

    description = models.TextField(
        'Опис',
        blank=True
    )

    pub_date = models.DateTimeField(
        'Дата публікації',
        default=timezone.now
    )

    slug = models.SlugField(
        'Slug',
        unique_for_date='pub_date'
    )

    main_page = models.BooleanField(
        'Головна сторінка',
        default=False
    )

    category = models.ForeignKey(
        Category,
        related_name='articles',
        on_delete=models.CASCADE,
        verbose_name='Категорія',
        blank=True,
        null=True
    )

    objects = models.Manager()

    class Meta:
        ordering = ['-pub_date']
        verbose_name = 'Стаття'
        verbose_name_plural = 'Статті'

    def __str__(self):
        return self.title

    def get_absolute_url(self):
        return reverse(
            'article-detail',
            kwargs={
                'year': self.pub_date.strftime('%Y'),
                'month': self.pub_date.strftime('%m'),
                'day': self.pub_date.strftime('%d'),
                'slug': self.slug,
            }
        )


class ArticleImage(models.Model):
    article = models.ForeignKey(
        Article,
        related_name='images',
        on_delete=models.CASCADE,
        verbose_name='Стаття'
    )

    image = models.ImageField(
        'Фото',
        upload_to='photos/'
    )

    title = models.CharField(
        'Заголовок',
        max_length=250,
        blank=True
    )

    class Meta:
        verbose_name = 'Фото для статті'
        verbose_name_plural = 'Фото для статей'

    def __str__(self):
        return self.title if self.title else self.filename

    @property
    def filename(self):
        return self.image.name.rsplit('/', 1)[-1]