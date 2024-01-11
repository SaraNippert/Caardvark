import { createStore as _createStore } from "vuex";
const NOTIFICATION_TIMEOUT = 6000;
import axios from "axios";

export function createStore(currentToken, currentUser) {
  let store = _createStore({
    state: {
      token: currentToken || "",
      user: currentUser || {},
      notification: null,
      cardFlipped: false,
    },
    mutations: {
      SET_NOTIFICATION(state, notification) {
        // Clear the current notification if one exists
        if (state.notification) {
          this.commit("CLEAR_NOTIFICATION");
        }
        if (typeof notification === "string") {
          // If only a string was sent, create a notification object with defaults
          notification = {
            message: notification,
            type: "error",
            timeout: NOTIFICATION_TIMEOUT,
          };
        } else {
          // Else add default values if needed
          notification.type = notification.type || "error";
          notification.timeout = notification.timeout || NOTIFICATION_TIMEOUT;
        }
        // Set the notification in state
        state.notification = notification;
        // Clear the message after timeout (see https://developer.mozilla.org/en-US/docs/Web/API/setTimeout)
        notification.timer = window.setTimeout(() => {
          this.commit("CLEAR_NOTIFICATION");
        }, notification.timeout);
      },
      CLEAR_NOTIFICATION(state) {
        if (state.notification && state.notification.timer) {
          window.clearTimeout(state.notification.timer);
        }
        state.notification = null;
      },
      SET_AUTH_TOKEN(state, token) {
        state.token = token;
        localStorage.setItem("token", token);
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      },
      SET_USER(state, user) {
        state.user = user;
        localStorage.setItem("user", JSON.stringify(user));
      },
      LOGOUT(state) {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        state.token = "";
        state.user = {};
        axios.defaults.headers.common = {};
      },
      FLIP_CARD(state) {
        state.cardFlipped = !state.cardFlipped;
      },   
      resetAnswerArrays(state) {
        state.rightAnswers = [];
        state.wrongAnswers = [];
      },
    },
  });
  return store;
}
