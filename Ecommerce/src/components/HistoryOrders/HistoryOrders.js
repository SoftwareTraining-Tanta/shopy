import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useParams } from 'react-router-dom'
import { buyProduct, deleteProduct, getCart } from '../../redux/cartSlice'
import {Container,Card, Image, Box, Title, Price, NavLink, Button, Buttons, ImageContainer} from '../Home/HomeStyle'
import {H2, Wrapper,style} from '../Cart/CartStyle'

function HistoryOrders() {
  const dispatch = useDispatch()
  let Username = sessionStorage.getItem('sign')  
  let {list} = useSelector((state)=> state.cart)
  useEffect(()=>{
      if (Username && list.length == 0) {
           fetch(`https://localhost:5001/api/clients/ClientHistory/${Username}`)
          .then(res=>res.json())
          .then(json=> json.filter((i)=>{
              fetch(`https://localhost:5001/api/models/Get/${i.model}`)
              .then(res=>res.json())
              .then(json=> dispatch(getCart({model: i.model, image: json.imagePath, clientUsername:i.clientUsername, rate: i.rate, count:1}))
              )
          }))
      }
  },[])
  console.log(list)
return (
    <Container>
        <H2>Your Products</H2>
        <Wrapper >
          {list.length ? (list.map((i)=>{
                return(
                    <Card key={Math.random()}>
                          <ImageContainer>
                              <Image src={i.image} alt="Background"/>
                          </ImageContainer>
                          <Box>
                              <Title>{i.model}</Title>
                              <Price>{i.count > 1 && `${i.count} items`}</Price>
                          </Box>
                  </Card>
                )
            })):(<div style={style}>There Aren't Products To Show</div>)}
        </Wrapper>
    </Container>
)
}

export default HistoryOrders