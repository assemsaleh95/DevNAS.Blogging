$(function () {
    var l = abp.localization.getResource('Blogging');
    var postService = devNAS.blogging.posts.post;
    var pageSizeDropdown = document.getElementById('pageSize');
    var createModal = new abp.ModalManager(abp.appPath + 'Posts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Posts/EditModal');


    var currentPage = 1; // Initial current page
    var totalPages = 5; // Initial total pages

    document.getElementById('pageSize').addEventListener('change', function () {
        updatePageSize();
    });

    function updatePageSize() {
        updateFilter({ maxResultCount: parseInt(pageSizeDropdown.value), skipCount: 0 });
        currentPage = 1; // Reset current page when changing page size
        getPosts();
    }

    refresh();

    function refresh() {
        updateFilter({ skipCount: (currentPage - 1) * parseInt(pageSizeDropdown.value) });
        getPosts();
    }

    function filter() {
        return {
            sorting: 'creationTime desc',
            maxResultCount: parseInt(pageSizeDropdown.value),
            skipCount: (currentPage - 1) * parseInt(pageSizeDropdown.value),
        };
    }

    function updateFilter(newFilter) {
        Object.assign(filter(), newFilter);
    }

    function getPosts() {
        console.log("Fetching posts...");

        postService.getList(filter())
            .then(function (result) {
                if (result && result.items) {
                    // Display posts in the postsContainer
                    renderPosts(result.items);

                    // Handle pagination
                    renderPagination(result);
                } else {
                    console.warn('No posts found.');
                }
            })
            .catch(function (error) {
                console.error('Error fetching data:', error);
            });
    }
    function renderPosts(posts) {
        var postsContainer = document.getElementById('postsContainer');
        postsContainer.innerHTML = '';

        posts.forEach(function (post) {
            var postCard = document.createElement('div');

            postCard.innerHTML = '<div class="card bg-light border-success">' +
                '<div class="card-header">' +
                '<h5 class="card-title">' + post.title + '</h5>' +
                '<h6 class="card-subtitle mb-2 text-muted">' + l('Enum:PostType.' + post.type) + '</h6>' +
                '<h6 class="card-subtitle mb-2 text-muted">' + post.authorName + '</h6>' +
                '</div>' +
                '<div class="card-body">' +
                '<h6 class="card-subtitle mb-2 text-muted">' + post.shortDescription + '</h6>' +
                '<p class="card-text">' + post.description + '</p>' +
                '</div>' +
                '<div class="card-footer text-muted">Publish Date: ' + luxon.DateTime.fromISO(post.publishDate, { locale: abp.localization.currentCulture.name }).toLocaleString() + '</div>' +
                '<div class="card-footer text-muted">Creation Time: ' + luxon.DateTime.fromISO(post.creationTime, { locale: abp.localization.currentCulture.name }).toLocaleString(luxon.DateTime.DATETIME_SHORT) + '</div>' +
                '<div class="card-footer">' +
                '<button class="btn btn-primary OpenEditModal" data-post-id="' + post.id + '">Edit</button>' +
                '<button class="btn btn-danger DeletePost" data-post-id="' + post.id + '">Delete</button>' +
                '</div>' +
                '</div>';

            postsContainer.appendChild(postCard);
        });
    }

    function renderPagination(result) {
        var paginationContainer = document.getElementById('paginationContainer');
        var totalRecords = result.totalCount;
        var totalPages = Math.ceil(totalRecords / parseInt(pageSizeDropdown.value));

        var paginationHTML = '<ul class="pagination">';
        for (var i = 1; i <= totalPages; i++) {
            paginationHTML += '<li class="page-item ' + (currentPage === i ? 'active' : '') + '"><a class="page-link" href="#" data-page="' + i + '">' + i + '</a></li>';
        }
        paginationHTML += '</ul>';

        paginationContainer.innerHTML = paginationHTML;

        // Event handling for pagination links
        $(paginationContainer).off('click', 'a.page-link').on('click', 'a.page-link', function (e) {
            e.preventDefault();
            var newPage = parseInt($(this).data('page'));
            if (newPage !== currentPage) {
                currentPage = newPage;
                refresh();
            }
        });
    }
    $(document).on('click', '.OpenEditModal', function () {
        var postId = $(this).data('post-id');
        editModal.open({
            Id: postId
        });
    });
    // Event handling for the "Delete" button
    $(document).on('click', '.DeletePost', function () {
        var postId = $(this).data('post-id');

        // Display a confirmation dialog
        abp.message.confirm(l('BookDeletionConfirmationMessage', post.title), function (isConfirmed) {
            if (isConfirmed) {
                // Call the delete method and refresh posts
                postService.delete(postId)
                    .then(function () {
                        abp.notify.info('Post deleted successfully.');
                        refresh(); // Refresh posts after deleting a post
                    })
                    .catch(function (error) {
                        console.error('Error deleting post:', error);
                        abp.notify.error('An error occurred while deleting the post.');
                    });
            }
        });
    });

    editModal.onResult(function () {
        refresh(); // Refresh posts after editing a post
    });


    createModal.onResult(function () {
        refresh(); // Refresh posts after creating a new post
    });


    $('#NewPostButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
