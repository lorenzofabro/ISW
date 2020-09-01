from rest_framework import viewsets

"""
example:

from .models import Book
from .serializer import BookSerializer
class BookViewSet(viewsets.ModelViewSet):
    queryset = Book.objects.all()
    serializer_class = BookSerializer
"""