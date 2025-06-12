// --- Глобальные переменные ---
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();


const messageList = document.querySelector('.message-list');
if (messageList && window.PerfectScrollbar) {
    window.PS = new PerfectScrollbar(messageList);
}


// --- Обработка входящих сообщений ---
connection.on("ReceiveMessage", function (messageContent, senderId, timestamp) {
    const isFriendMessage = senderId === gfriendId;
    console.log("senderId:", senderId, typeof senderId);
    console.log("gfriendId:", gfriendId, typeof gfriendId);
    console.log(isFriendMessage);
    console.log(gfriendId);
    const messageClass = isFriendMessage ? 'text-friends' : 'text-author';

    const messageHtml = `
        <li class="${messageClass}">
            <p>${escapeHtml(messageContent)}</p>
            <div class="message-time">${formatTime(timestamp)}</div>
        </li>
    `;

    // Добавляем сообщение
    $('.message-list').append(messageHtml);


    const container = document.querySelector('.message-list');
    console.log("Элемент:", container);
    if (!container) {
        console.error("Не найден .message-list");
        return;
    }


    setTimeout(() => {
        scrollToBottom();
    }, 0);

});

// --- Форматирование времени ---
function formatTime(timestamp) {
    if (!timestamp) return '';

    const time = new Date(timestamp);
    if (isNaN(time.getTime())) return '';

    const now = new Date();
    const diffMs = now - time;
    const diffMin = Math.floor(diffMs / 60000);
    return `${diffMin} мин назад`;
}

// --- Безопасный вывод текста ---
function escapeHtml(text) {
    return $('<div>').text(text).html();
}

// --- Прокрутка вниз ---
function scrollToBottom() {
    const container = document.querySelector('.message-list');
    if (!container) return;

    container.scrollTop = container.scrollHeight;

    if (window.PS && typeof window.PS.update === 'function') {
        window.PS.update(container);
    }
}


// --- Подключение к SignalR и присоединение к чату ---
async function start(currentChatId) {
    try {
        // Проверяем, не находится ли соединение в состоянии "подключения" или "подключено"
        if (connection.state === signalR.HubConnectionState.Connected) {
            console.log("Уже подключено.");
            await connection.invoke("JoinChat", currentChatId);
            return;
        }

        if (connection.state === signalR.HubConnectionState.Connecting) {
            console.log("Соединение уже в процессе...");
            // Можно подождать немного и выйти, или просто выйти
            return;
        }

        // Только если соединение отключено — запускаем его
        await connection.start();
        console.log("SignalR Connected.");
        await connection.invoke("JoinChat", currentChatId);

        window.currentChatId = currentChatId;

    } catch (err) {
        console.error(err);
        setTimeout(() => start(currentChatId), 5000); // Переподключение при ошибке
    }
}

// --- Отправка сообщения ---
async function SendMessage(chatId, message) {
    try {
        await connection.invoke("SendMessage", chatId, message);
        console.log("Message sent.");
    } catch (err) {
        console.error("Ошибка отправки:", err);
    }
}


// --- Отправка по Enter ---
$('.live-chat-field').on('keydown', function (e) {
    if (e.key === 'Enter' && !e.shiftKey) {
        e.preventDefault();
        $('.chat-message-send').trigger('click');
    }
});


function SetChatTitle(response) {
    console.log(response);
    const friend = response.friendName;
    const liveChatTitleHtml = `
        <div class="profile-thumb">
            <a href="/profile/${response.friendId}">
                <figure class="profile-thumb-small profile-active">
                    <img src="${response.photoUrl}" alt="profile picture">
                </figure>
            </a>
        </div>
        <div class="posted-author">
            <h6 class="author"><a href="profile/${response.friendId}">${escapeHtml(friend)}</a></h6>
        </div>
        <div class="live-chat-settings ml-auto">
            <button class="close-btn" data-close="chat-output-box"><i class="flaticon-cross-out"></i></button>
        </div>
    `;

    $('.live-chat-title').html(liveChatTitleHtml);
}

$(document).on('click', '.close-btn', function () {
    const targetClass = $(this).data('close');
    $('.' + targetClass).removeClass('show');
});


    document.getElementById('photo-upload').addEventListener('change', function () {
    const fileName = this.files[0]?.name || '';
    document.getElementById('file-name-display').textContent = fileName;
});