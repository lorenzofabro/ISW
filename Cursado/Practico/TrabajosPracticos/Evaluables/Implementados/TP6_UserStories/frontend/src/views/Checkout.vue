<template>
  <v-container>
    <v-card outlined>
      <v-container fluid class="ma-0 mb-5" style="background-color:#1F9180">
        <v-card-title
          class="title text-uppercase font-weight-bold white--text"
          style="font-family: ProximaNova !important; word-break: break-word;"
          >Checkout</v-card-title
        >
      </v-container>
      <v-form ref="form" v-model="valid" lazy-validation>
        <Divider title="Dirección" />
        <v-row class="rowMargin">
          <v-col cols="6">
            <v-text-field
              label="Calle *"
              outlined
              :rules="[(v) => !!v || 'La calle es un campo requerido']"
              required
            ></v-text-field>
            <v-select
              label="Ciudad *"
              outlined
              v-model="ciudad"
              :items="ciudades"
              :rules="[(v) => !!v || 'Debe seleccionar una ciudad']"
              required
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-text-field
              label="Número *"
              outlined
              :rules="[(v) => !!v || 'El número es un campo requerido']"
              required
            ></v-text-field>
            <v-text-field label="Referencias" outlined></v-text-field>
          </v-col>
        </v-row>
        <Divider title="Horario de entrega" />
        <v-row class="rowMargin">
          <v-col cols="6">
            <v-select
              label="Horario de entrega *"
              outlined
              v-model="formaEntrega"
              :items="formasEntrega"
              :rules="[(v) => !!v || 'Debe seleccionar un horario de entrega']"
              required
            ></v-select>
          </v-col>
          <v-col cols="3">
            <v-menu
              v-if="formaEntrega === 'Programar'"
              ref="menu"
              v-model="menu"
              :close-on-content-click="false"
              :return-value.sync="date"
              transition="scale-transition"
              offset-y
              min-width="290px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                  v-model="date"
                  label="Fecha *"
                  outlined
                  readonly
                  v-bind="attrs"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker v-model="date" no-title scrollable>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="menu = false"
                  >Cancelar</v-btn
                >
                <v-btn text color="primary" @click="$refs.menu.save(date)"
                  >OK</v-btn
                >
              </v-date-picker>
            </v-menu>
          </v-col>
          <v-col cols="3">
            <v-menu
              v-if="formaEntrega === 'Programar'"
              ref="menu2"
              v-model="menu2"
              :close-on-content-click="false"
              :nudge-right="40"
              :return-value.sync="time"
              transition="scale-transition"
              offset-y
              max-width="290px"
              min-width="290px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                  v-model="time"
                  label="Hora *"
                  outlined
                  readonly
                  v-bind="attrs"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-time-picker
                format="24hr"
                v-if="menu2"
                v-model="time"
                full-width
                @click:minute="$refs.menu2.save(time)"
              ></v-time-picker>
            </v-menu>
          </v-col>
        </v-row>
        <Divider title="Forma de pago" />
        <v-row class="rowMargin">
          <v-col cols="6">
            <v-select
              label="Forma de pago *"
              outlined
              v-model="formaPago"
              :items="formasPago"
              :rules="[(v) => !!v || 'Debe seleccionar una forma de pago']"
              @change="
                formaPago === 'Tarjeta de crédito' ? (dialog = true) : ''
              "
              required
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-text-field
              label="Pago con *"
              outlined
              v-if="formaPago === 'Efectivo'"
              :rules="[
                (v) =>
                  !!v || 'Debe ingresar la cantidad con la que vas a a pagar',
              ]"
              :required="formaPago === 'Efectivo'"
            ></v-text-field>
            <v-btn
              v-if="formaPago === 'Tarjeta de crédito'"
              class="mt-3"
              dark
              @click="dialog = true"
              >Cambiar datos</v-btn
            >
          </v-col>
        </v-row>
        <v-dialog v-model="dialog" max-width="1024px">
          <v-card color="#EBEBEB" style="margin-top: -60px">
            <CreditCard ref="creditCard" :closeDialog="closeDialog" />
          </v-card>
        </v-dialog>
      </v-form>
    </v-card>
    <v-container class="mt-5 text-center">
      <v-btn color="#1F9180" class="mr-4 white--text" @click="realizarPedido">
        Realizar pedido
      </v-btn>
      <v-btn color="error" :to="{ name: 'Main' }">
        Volver
      </v-btn>
    </v-container>
  </v-container>
</template>

<style>
.rowMargin {
  margin-bottom: -10px;
  margin-left: 5px;
  margin-right: 5px;
}
</style>

<script>
import Divider from "../components/Divider";
import swal from "sweetalert";
import CreditCard from "./CreditCard";
export default {
  components: { Divider, CreditCard },
  data() {
    return {
      card: false,
      dialog: false,
      ciudad: "",
      ciudades: [
        "Córdoba",
        "Catamarca",
        "Choele Choel",
        "Alcira Gigena",
        "Plottier",
      ],
      formaPago: "",
      formasPago: ["Efectivo", "Tarjeta de crédito"],
      formaEntrega: "",
      formasEntrega: ["Lo antes posible", "Programar"],
      menu: false,
      menu2: false,
      date: new Date().toISOString().substr(0, 10),
      time: "14:30",
      valid: true,
    };
  },
  methods: {
    realizarPedido() {
      let validate = this.$refs.form.validate();
      if (this.formaPago === "Tarjeta de crédito" && !this.card) {
        swal("Los datos de la tarjeta no son correctos 😢", "", "error");
      } else if (validate) {
        swal(
          "Pedido realizado exitosamente 😁",
          "¡Tu pedido está en camino!",
          "success"
        );
        this.$router.push({ name: "OrderSuccess" });
      }
    },
    closeDialog() {
      this.card = true;
      this.dialog = false;
    },
  },
};
</script>
