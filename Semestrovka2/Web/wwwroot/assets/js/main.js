(function ($) {
	"use strict";

	$(".msg-trigger-btn").on("click", function (event) {
		event.stopPropagation();
		event.preventDefault();
		var $this = $(this);
		var $prevTartget = $(this).parent().siblings().children(".msg-trigger-btn").attr('href');
		var target = $this.attr('href');
		$(target).slideToggle();
		$($prevTartget).slideUp();

	});

	//Close When Click Outside
	$('body').on('click', function (e) {
		var $target = e.target;
		if (!$($target).is('.message-dropdown') && !$($target).parents().is('.message-dropdown')) {
			$(".message-dropdown").slideUp("slow");
		}
	});

	//Background Image JS start
	var bgSelector = $(".bg-img");
	bgSelector.each(function (index, elem) {
		var element = $(elem),
			bgSource = element.data('bg');
		element.css('background-image', 'url(' + bgSource + ')');
	});

	// video player active js
	var plyrVideo = new Plyr('.plyr-video'),
		plyrAudio = new Plyr('.plyr-audio'),
		plyrYoutube = new Plyr('.plyr-youtube'),
		plyrVimeo = new Plyr('.plyr-vimeo');

	// active profile carousel js
	$('.active-profile-carousel').slick({
		speed: 800,
		slidesToShow: 10,
		prevArrow: '<button type="button" class="slick-prev"><i class="bi bi-arrow-left-rounded"></i></button>',
		nextArrow: '<button type="button" class="slick-next"><i class="bi bi-arrow-right-rounded"></i></button>',
		responsive: [{
			breakpoint: 1200,
			settings: {
				slidesToShow: 5,
			}
		},
			{
				breakpoint: 992,
				settings: {
					slidesToShow: 8,
				}
			}]
	});

	// active profile carousel js
	$('.active-profile-mobile').slick({
		speed: 800,
		slidesToShow: 6,
		arrows: false,
		responsive: [{
			breakpoint: 480,
			settings: {
				slidesToShow: 4,
			}
		}]
	});

	// active profile carousel js
	$('.favorite-item-carousel').slick({
		autoplay: true,
		speed: 800,
		slidesToShow: 5,
		arrows: false,
		responsive: [{
			breakpoint: 992,
			settings: {
				slidesToShow: 3,
			}
		},
			{
				breakpoint: 576,
				settings: {
					slidesToShow: 2,
				}
			}]
	});

	// live chat box and friend search box active js
	$(".profile-active").on('click', function () {
		$(".chat-output-box").addClass('show');
	})
	$(".search-field").on('click', function () {
		$(".friend-search-list").addClass('show');
	})
	$(".close-btn").on('click', function () {
		var $this = $(this),
			$target = $this.data('close');
		$('.' + $target).removeClass('show');
	})

	// mobile header seach box active
	$(".search-trigger").on('click', function () {
		$('.search-trigger, .mob-search-box').toggleClass('show');
	})

	$(".chat-trigger, .close-btn").on('click', function () {
		$('.mobile-chat-box').toggleClass('show');
	})
	$(".request-trigger").on('click', function () {
		$('.frnd-request-list').toggleClass('show');
	})

	// mobile friend search active js
	$(".search-toggle-btn").on('click', function () {
		$('.mob-frnd-search-inner').toggleClass('show');
	})

	// profile dropdown triger js
	$('.profile-triger').on('click', function (event) {
		event.stopPropagation();
		$(".profile-dropdown").slideToggle();
	})

	//Close When Click Outside
	$('body').on('click', function (e) {
		var $target = e.target;
		if (!$($target).is('.profile-dropdown') && !$($target).parents().is('.profile-dropdown')) {
			$(".profile-dropdown").slideUp("slow");
		}
	});

	// perfect scroll bar js
	$('.custom-scroll').each(function () {
		var ps = new PerfectScrollbar($(this)[0]);
	});


	// light gallery active js
	$(document).ready(function () {
		$(".img-popup").lightGallery();

		// light gallery images
		$(".img-gallery").lightGallery({
			selector: ".gallery-selector",
			hash: false
		});
	});

	$('.gallery-toggle').on('click', function () {

		var productThumb = $(this).find(".product-thumb-large-view img"),
			imageSrcLength = productThumb.length,
			images = [];
		for (var i = 0; i < imageSrcLength; i++) {
			images[i] = {"src": productThumb[i].src, "thumb": productThumb[i].src};
		}

		$(this).lightGallery({
			dynamic: true,
			actualSize: false,
			hash: false,
			index: 0,
			dynamicEl: images
		});

	});

	// photo filter active js
	$('.photo-filter').imagesLoaded(function () {
		var $grid = $('.photo-filter, .friends-list').isotope({});
		// filter items on button click
		$('.filter-menu').on('click', 'button', function () {
			var filterValue = $(this).attr('data-filter');
			$grid.isotope({filter: filterValue});
			$(this).siblings('.active').removeClass('active');
			$(this).addClass('active');
		});

	});

	// nice select active js
	$('select').niceSelect();

	// Scroll to top active js
	$(window).on('scroll', function () {
		if ($(this).scrollTop() > 600) {
			$('.scroll-top').removeClass('not-visible');
		} else {
			$('.scroll-top').addClass('not-visible');
		}
	});
	$('.scroll-top').on('click', function (event) {
		$('html,body').animate({
			scrollTop: 0
		}, 1000);
	});


	$('#email').bind("cut copy paste", function (e) {
		e.preventDefault();
	});

})(jQuery);

$(function () {
	$('#friend-search-form').on('submit', function (e) {
		e.preventDefault();
		let query = $(this).find('.search-field').val();

		$.ajax({
			url: '/api/searchFriends', // Убедись, что путь правильный
			type: 'GET',
			data: {query: query},
			success: function (result) {
				$('#search-results').html(result);
			},
			error: function () {
				$('#search-results').html('<p>Error loading friends</p>');
			}
		});
	});
});


$(document).on('click', '.add-frnd', function () {
	const button = $(this);
	const friendId = button.data('id');
	const fUrl = button.data('url');

	$.ajax({
		url: '/api/addFriend',
		type: 'POST',
		data: {friendId: friendId},
		success: function (response) {
			console.log(response);
			if (response && response > 0) {
				button.text('Friend Added').prop('disabled', true);
				const newSlide = `
					<div class="single-slide">
						<div class="profile-thumb active profile-active">
							<a href="javascript:void(0)">
								<figure class="profile-thumb-small">
									<img src="${fUrl}" alt="profile picture">
								</figure>
							</a>
						</div>
					</div>
				`;

				// Добавление в Slick-карусель
				$('.active-profile-carousel').slick('slickAdd', newSlide);

			} else {
				button.text('Try Later');
			}
		},
		error: function () {
			button.text('Try Later');
		}
	});
});

let chatId = null;
let gfriendId = null;
$(document).ready(function () {
	// --- Единожды назначаем обработчик отправки ---
	$('.chat-message-send').on('click', function (e) {
		e.preventDefault();

		const messageInput = $('#chatik');
		let message = messageInput.val().trim();

		if (!message || !chatId) {
			console.warn("Сообщение пустое или chatId не задан");
			return;
		}

		SendMessage(chatId, message);
		messageInput.val('');
	});

	// --- Обработчик клика по другу ---
	$('.friend-photo').on('click', function (e) {
		e.preventDefault();
		const friendId = $(this).data('friend-id');
		gfriendId = friendId;

		if (!friendId) return;

		$.ajax({
			url: '/api/chats/getByFriendId/' + friendId,
			type: 'GET',
			success: function (response) {
				const currentChatId = response.chat.id;
				chatId = currentChatId;
				start(chatId);
				
				SetChatTitle(response);

				const messagesList = $('.message-list');
				messagesList.empty();

				response.messages.forEach(function (message) {
					const isFriendMessage = message.senderId === friendId;
					const messageClass = isFriendMessage ? 'text-friends' : 'text-author';

					const messageHtml = `
                        <li class="${messageClass}">
                            <p>${escapeHtml(message.content)}</p>
                            <div class="message-time">${formatTime(message.timestamp)}</div>
                        </li>
                    `;

					messagesList.append(messageHtml);
				});

				$('.message-list-inner').scrollTop($('.message-list-inner')[0].scrollHeight);
			},
			error: function (xhr, status, error) {
				console.error('Ошибка загрузки сообщений:', error);
			}
		});
	});

	// Форматирование времени (в минутах назад, пример)
	function formatTime(timestamp) {
	const time = new Date(timestamp);
	const now = new Date();
	const diffMs = now - time;
	const diffMin = Math.floor(diffMs / 60000);
	return `${diffMin} минут назад`;
}

	// Безопасный вывод текста
	function escapeHtml(text) {
	return $('<div>').text(text).html();
}
});