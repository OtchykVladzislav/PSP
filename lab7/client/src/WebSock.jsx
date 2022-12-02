import React, {useEffect, useRef, useState} from 'react';
import { useParams } from 'react-router-dom';
import classes from './App.css'
import MyModal from './MyModal';


const WebSock = () => {
    const [messages, setMessages] = useState([]);
    const [value, setValue] = useState('');
    const socket = useRef()
    const [connected, setConnected] = useState(false);
    const [username, setUsername] = useState('')
    const params = useParams()


    function connect() {
        socket.current = new WebSocket('ws://localhost:5000')
        socket.current.onopen = () => {
            setConnected(true)
            const message = {
                event: 'connection',
                username,
                id: params.id
            }
            socket.current.send(JSON.stringify(message))
        }
        socket.current.onmessage = (event) => {
            const message = JSON.parse(event.data)
            setMessages(prev => [message, ...prev])
        }
        socket.current.onclose= () => {
            console.log('Socket закрыт')
        }
        socket.current.onerror = () => {
            console.log('Socket произошла ошибка')
        }
    }

    const sendMessage = async () => {
        const message = {
            username,
            message: value,
            id: params.id,
            event: 'message'
        }
        socket.current.send(JSON.stringify(message));
        setValue('')
    }

    return (
        <article>
            <MyModal visible={connected} >
                <input className='inputName' value={username} onChange={e => setUsername(e.target.value)} type="text" placeholder="Введите ваше имя"/>
                <button onClick={connect}>Войти</button>
            </MyModal>
            <div className='chat'>
                <div className="messages">
                    {messages.map(mess =>
                        <div key={mess.id}>
                            {mess.event === 'connection'
                                ? 
                                <div className="connection_message">Пользователь {mess.username} подключился</div>
                                : 
                                <div className="messages">
                                    <span className='userName'>{mess.username}</span><span className='message'dangerouslySetInnerHTML={{ __html:mess.message}}/>
                                </div>
                            }
                        </div>
                    )}
                </div>
            </div>
            <div className="form">
                <input value={value} onChange={e => setValue(e.target.value)} type="text"/>
                <button onClick={sendMessage}></button>
            </div>
        </article>
    );
};

export default WebSock;