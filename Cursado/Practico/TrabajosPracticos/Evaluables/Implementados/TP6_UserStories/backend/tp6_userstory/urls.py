from rest_framework import routers

"""
example
from .viewset import BookViewSet
"""

router = routers.SimpleRouter()

"""
example continues
router.register('books', BookViewSet)
"""

urlpatterns = router.urls
