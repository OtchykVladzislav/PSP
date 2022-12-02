import React from 'react';
import classes from "./MyModal.module.css"

export default function MyModal({children, visible}){
    const rootClasses = [classes.myModal]

    if (!visible) {
        rootClasses.push(classes.active);
    }
    return(
        <div className={rootClasses.join(' ')}>
            <div className={classes.myModalContent} onClick={(e) => e.stopPropagation()}>
                {children}
            </div>
        </div>
    )
}