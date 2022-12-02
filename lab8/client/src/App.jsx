import { useEffect } from 'react'
import { useState } from 'react'
import './App.css'
import AddElement from './components/AddElement'
import EditElement from './components/EditElement'
import MyModal from './components/MyModal/MyModal'
import RequestList from './components/RequestList'


function App() {
  const [list, setList] = useState([])
  const [modal, setModal] = useState(false)
  const [editObj, setEditObj] = useState({})

  const read = async () => {
    const response = await RequestList.getAll()
    setList([...list, ...response.data])
  }

  const del = async (id) => {
    const response = await RequestList.delById(id)
    setList(list.filter(e => e._id != id))
  }

  const editBool = (newObj) => {
    setEditObj({...editObj, ...newObj})
    setModal(true)
}

  const edit = async (newObj) => {
    await RequestList.putById(newObj._id, newObj)
    setList([...list.filter(e => e._id != newObj._id), newObj])
    setModal(false)
  }

  const add = async (newObj) => {
    const response = await RequestList.addElem(newObj)
    const value = {...newObj, _id: response.data.insertedId}
    setList([...list, value])
  }

  useEffect(() => read, [])

  return (
    <div className="App">
      <div className='header'>.Taxi</div>
      <div className='title'>Добавление</div>
      <AddElement add={add}/>
      <MyModal visible={modal} setVisible={setModal}>
        <EditElement obj={editObj} visible={modal} func={edit}/>
      </MyModal>
      <div>
        {!list.length?
          <div className='title'>Список пуст!</div>
          :
          list.map(e => 
              <div className='elemList' key={e._id}>
                <div>{e.secondname} {e.name} {e.thirdname}</div>
                <div>{e.tariff} р.</div>
                <div>{e.volume} п.</div>
                <div>{e.price} p.</div>
                <div>
                  <button className='but' onClick={() => del(e._id)}>Del</button>
                  <button className='but' onClick={() => editBool(e)}>Edit</button>
                </div>
              </div>
          )
        }
      </div>
    </div>
  )
}

export default App
