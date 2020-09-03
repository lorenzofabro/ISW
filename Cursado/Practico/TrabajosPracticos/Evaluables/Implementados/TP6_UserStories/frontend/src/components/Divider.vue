<template>
  <v-responsive class="mx-5 mb-2">
    <span style="font-size: 18px" class="text-uppercase d-flex">{{
      title
    }}</span>
    <v-divider style="border: 1px solid #1F9180;"></v-divider>
  </v-responsive>
</template>

<script>
export default {
  props: ["title"],
};
</script>
