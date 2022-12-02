import React from "react";
import { useState, useEffect} from "react";

export default function EditElement({...props}){
    const [addElem, setAddElem] = useState({...props.obj})

    const send = () => {
        props.func(addElem)
    }

    useEffect(() => {if(props.visible) setAddElem({...props.obj})}, [props.visible])

    return(
        <div className='form'>
            <input type="text" value={addElem.secondname} onChange={e => setAddElem({...addElem, secondname: e.target.value})} placeholder="Фамилия..."/>
            <input type="text" value={addElem.name} onChange={e => setAddElem({...addElem, name: e.target.value})} placeholder="Имя..."/>
            <input type="text" value={addElem.thirdname} onChange={e => setAddElem({...addElem, thirdname: e.target.value})} placeholder="Отчество..."/>
            <input type="number" value={addElem.tariff} onChange={e => setAddElem({...addElem, tariff: e.target.value})} placeholder="Тариф..."/>
            <input type="number" value={addElem.volume} onChange={e => setAddElem({...addElem, volume: e.target.value})} placeholder="Объем работы..."/>
            <input type="number" value={addElem.price} onChange={e => setAddElem({...addElem, price: e.target.value})} placeholder="Одна поездка..."/>
            <button className='but' onClick={send}>Редактировать</button>
        </div>
    )
}