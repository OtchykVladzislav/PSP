import React from 'react';
import {BrowserRouter, Routes, Route, Navigate} from 'react-router-dom'
import WebSock from './WebSock';

const App = () => {
    const id = `f${(+new Date).toString(16)}`
    return (
        <BrowserRouter>
            <div className="app">
                <Routes>
                    <Route path='/:id' element={<WebSock/>}/>
                    <Route path="*" element={<Navigate to={id} replace={true}/>}/>
                </Routes>
            </div>
        </BrowserRouter>
    );
};

export default App;